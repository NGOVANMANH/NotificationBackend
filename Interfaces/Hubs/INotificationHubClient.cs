using notify.Dtos;

namespace notify.Interfaces;

public interface INotificationHubClient
{
    Task ReceiveNotification(NotificationDto notificationDto);
    Task ReceiveOnlineSubscribers(IEnumerable<Guid> userIds);
}
