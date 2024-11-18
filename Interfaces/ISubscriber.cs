using notify.Dtos;

namespace notify.Interfaces
{
    public interface ISubscriber
    {
        Task UpdateAsync(NotificationDto notification, object? options = null);
    }
}