using Microsoft.EntityFrameworkCore;
using notify.Data;
using notify.Interfaces;
using notify.Models;

namespace notify.Repositories;
public class NotificationRepository : INotificationRepository
{
    private readonly ApiContext _context;
    public NotificationRepository(ApiContext context)
    {
        _context = context;
    }

    public async Task<Notification> CreateNotificationAsync(Notification notification)
    {
        var result = await _context.Notifications.AddAsync(notification);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Notification?> GetNotificationAsync(Guid id)
    {
        var notification = await _context.Notifications.Include(notification => notification.User).FirstOrDefaultAsync();

        return notification;
    }
}