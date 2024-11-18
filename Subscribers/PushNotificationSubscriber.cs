using notify.Dtos;
using notify.Interfaces;

namespace notify.Services;

public class PushNotificationSubscriber : ISubscriber
{
    public async Task UpdateAsync(NotificationDto notification, object? options = null)
    {
        await SendPushNotificationAsync(notification);
    }

    private Task SendPushNotificationAsync(NotificationDto notification)
    {
        throw new NotImplementedException();
    }
}