using System.ComponentModel.DataAnnotations;
using notify.Models;

namespace notify.Dtos;

public class NotificationReqDto
{
    [Required]
    public string Message { get; set; } = null!;
    [Required]
    public IEnumerable<Guid> ToUserIds { get; set; } = null!;
    public IEnumerable<Channel> Channels { get; set; } = new List<Channel>();
    public NotificationType Type { get; set; } = NotificationType.INFO;
}