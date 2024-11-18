using notify.Dtos;
using notify.Interfaces;

namespace notify.Services;

public class SMSNotificationSubscriber : ISubscriber
{
    public async Task UpdateAsync(NotificationDto notification, object? options = null)
    {
        await SendSMSNotification(notification);
    }

    private Task SendSMSNotification(NotificationDto notification)
    {
        throw new NotImplementedException();
    }
}