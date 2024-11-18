using notify.Configs;
using notify.Dtos;
using notify.Interfaces;

namespace notify.Subjects;

public class NotificationPublisher : IPublisher
{
    private readonly ThreadSafeList<ISubscriber> _subscribers = new ThreadSafeList<ISubscriber>();

    public async Task NotifySubscribersAsync(NotificationDto notification, object? options = null)
    {
        foreach (var subscriber in _subscribers.ToList())
        {
            await subscriber.UpdateAsync(notification, options);
        }
    }

    public void Subscribe(ISubscriber subscriber)
    {
        _subscribers.Add(subscriber);
    }

    public void UnSubscribe(ISubscriber subscriber)
    {
        _subscribers.Remove(subscriber);
    }

    public void UnSubscribeAll()
    {
        _subscribers.Clear();
    }
}