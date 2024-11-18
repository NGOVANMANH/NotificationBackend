using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace notify.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public Guid? PreferenceId { get; set; }
    [ForeignKey("PreferenceId")]
    public Preference? Preference { get; set; }
}