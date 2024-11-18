using Microsoft.AspNetCore.SignalR;
using notify.Dtos;
using notify.Hubs;
using notify.Interfaces;

namespace notify.Services;

public class InAppNotificationSubscriber : ISubscriber
{
    private readonly IHubContext<NotificationHubClient> _hubContext;

    public InAppNotificationSubscriber(IHubContext<NotificationHubClient> hubContext)
    {
        _hubContext = hubContext;
    }
    public async Task UpdateAsync(NotificationDto notification, object? options = null)
    {
        await SendInAppNotificationAsync(notification);
    }

    private async Task SendInAppNotificationAsync(NotificationDto notification)
    {
        if (notification.UserId != Guid.Empty)
        {
            var connectionId = NotificationHubClient.ConnectedUsers
                .FirstOrDefault(pair => pair.Key == notification.UserId).Value;

            if (!string.IsNullOrEmpty(connectionId))
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", notification);
            }
        }
        else
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        }
    }
}