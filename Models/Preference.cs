using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace notify.Models;

public class Preference
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
    public bool NotifyViaEmail { get; set; }
    public bool NotifyViaSMS { get; set; }
    public bool NotifyViaPush { get; set; }
    public bool NotifyViaInApp { get; set; }
    public bool? DoNotDisturb { get; set; }
}