using notify.Models;

namespace notify.Dtos;

public class NotificationDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public string Message { get; set; } = null!;
    public NotificationType Type { get; set; }
    public DateTime CreatedAt { get; set; }
}