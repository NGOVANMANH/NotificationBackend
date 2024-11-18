using notify.Dtos;
using notify.Exceptions;
using notify.Interfaces;
using notify.Models;

namespace notify.Services;

public class NotificationService : INotificationService
{
    private readonly IPublisher _publisher;
    private readonly IEnumerable<ISubscriber> _subscribers;
    private readonly IServiceProvider _serviceProvider;

    public NotificationService(IServiceProvider serviceProvider, IPublisher publisher, IEnumerable<ISubscriber> subscribers)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        _subscribers = subscribers ?? throw new ArgumentNullException(nameof(subscribers));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public async Task CreateNotificationAsync(NotificationReqDto notificationDto)
    {
        var users = await GetValidUsersAsync(notificationDto.ToUserIds);
        if (!users.Any())
            throw new GlobalException("No valid users found for the provided ToUserIds.", System.Net.HttpStatusCode.BadRequest);

        foreach (var user in users)
        {
            if (IsUserInDoNotDisturbMode(user)) continue;

            var notifications = CreateNotificationsForUser(user, notificationDto);

            var createdNotifications = await SaveNotificationsAsync(notifications);

            if (createdNotifications.Any())
            {
                NotifySubscribers(createdNotifications.First());
            }
        }
    }

    private async Task<List<User>> GetValidUsersAsync(IEnumerable<Guid> userIds)
    {
        var userResults = await Task.WhenAll(userIds.Select(GetUserByIdAsync));
        return userResults.Where(user => user != null).ToList()!;
    }

    private async Task<User?> GetUserByIdAsync(Guid userId)
    {
        using var scope = _serviceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        return await userService.GetUserAsync(userId);
    }

    private bool IsUserInDoNotDisturbMode(User user) => user.Preference?.DoNotDisturb == true;

    private List<Notification> CreateNotificationsForUser(User user, NotificationReqDto notificationDto)
    {
        var notifications = new List<Notification>();

        foreach (var channel in notificationDto.Channels)
        {
            if (ShouldNotifyUserViaChannel(user, channel))
            {
                SubscribeNotificationChannel(channel);

                notifications.Add(new Notification
                {
                    Id = Guid.NewGuid(),
                    Channel = channel,
                    IsRead = false,
                    Message = notificationDto.Message,
                    Type = notificationDto.Type,
                    UserId = user.Id,
                });
            }
        }

        return notifications;
    }

    private bool ShouldNotifyUserViaChannel(User user, Channel channel) => channel switch
    {
        Channel.EMAIL => user.Preference?.NotifyViaEmail == true,
        Channel.IN_APP => user.Preference?.NotifyViaInApp == true,
        Channel.PUSH => user.Preference?.NotifyViaPush == true,
        Channel.SMS => user.Preference?.NotifyViaSMS == true,
        _ => false
    };

    private async Task<List<Notification>> SaveNotificationsAsync(List<Notification> notifications)
    {
        var saveTasks = notifications.Select(async notification =>
        {
            using var scope = _serviceProvider.CreateAsyncScope();
            var notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
            return await notificationRepository.CreateNotificationAsync(notification);
        });

        var results = await Task.WhenAll(saveTasks);
        return results.Where(notification => notification != null).ToList()!;
    }

    private void SubscribeNotificationChannel(Channel channel)
    {
        var subscriber = GetSubscriber(channel);
        if (subscriber == null)
            throw new GlobalException($"No subscriber found for channel {channel}.", System.Net.HttpStatusCode.InternalServerError);

        _publisher.Subscribe(subscriber);
    }

    private ISubscriber? GetSubscriber(Channel channel) => channel switch
    {
        Channel.EMAIL => _subscribers.OfType<EmailNotificationSubscriber>().FirstOrDefault(),
        Channel.IN_APP => _subscribers.OfType<InAppNotificationSubscriber>().FirstOrDefault(),
        Channel.PUSH => _subscribers.OfType<PushNotificationSubscriber>().FirstOrDefault(),
        Channel.SMS => _subscribers.OfType<SMSNotificationSubscriber>().FirstOrDefault(),
        _ => throw new GlobalException($"Unsupported notification channel: {channel}.", System.Net.HttpStatusCode.BadRequest)
    };

    private void NotifySubscribers(Notification notification)
    {
        var notificationDto = new NotificationDto
        {
            Id = notification.Id,
            UserId = notification.UserId,
            User = notification.User,
            CreatedAt = notification.CreatedAt,
            Message = notification.Message,
            Type = notification.Type,
        };

        _publisher.NotifySubscribersAsync(notificationDto);
        _publisher.UnSubscribeAll();
    }
}
