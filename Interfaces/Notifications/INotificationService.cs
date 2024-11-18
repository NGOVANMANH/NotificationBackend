using notify.Dtos;
using notify.Models;

namespace notify.Interfaces;

public interface INotificationService
{
    Task CreateNotificationAsync(NotificationReqDto notificationDto);
}