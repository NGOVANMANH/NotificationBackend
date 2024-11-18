using System.ComponentModel.DataAnnotations;

namespace notify.Dtos;

public class UserCreateDto
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Phone { get; set; } = null!;
    public Guid? PreferenceId { get; set; }
}