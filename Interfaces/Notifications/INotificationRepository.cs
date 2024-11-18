using notify.Models;

namespace notify.Interfaces;

public interface INotificationRepository
{
    Task<Notification> CreateNotificationAsync(Notification notification);
    Task<Notification?> GetNotificationAsync(Guid id);
}