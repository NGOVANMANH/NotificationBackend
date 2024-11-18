using notify.Dtos;

namespace notify.Interfaces
{
    public interface IPublisher
    {
        void Subscribe(ISubscriber subscriber);
        void UnSubscribe(ISubscriber subscriber);
        Task NotifySubscribersAsync(NotificationDto notification, Object? options = null);
        void UnSubscribeAll();
    }
}