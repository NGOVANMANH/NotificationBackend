using System.ComponentModel.DataAnnotations;

namespace notify.Dtos;

public class PreferenceCreateDto
{
    [Required]
    public Guid UserId { get; set; }
    public bool NotifyViaEmail { get; set; } = true;
    public bool NotifyViaSMS { get; set; } = true;
    public bool NotifyViaPush { get; set; } = true;
    public bool NotifyViaInApp { get; set; } = true;
    public bool DoNotDisturb { get; set; } = false;
}