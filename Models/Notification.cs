using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace notify.Models;

public enum NotificationType
{
    INFO,
    WARNING,
    ALERT,
}

public enum Channel
{
    EMAIL,
    SMS,
    PUSH,
    IN_APP,
}

public class Notification
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
    public string Message { get; set; } = null!;
    public NotificationType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
    public Channel Channel { get; set; }
}