using System.ComponentModel.DataAnnotations;

namespace Pro.Structure.Core.Entities;

public class User : BaseEntity
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }

    public string Role { get; set; } = "User";
    public bool IsActive { get; set; } = true;
    public DateTime? LastLogin { get; set; }
    public int FailedLoginAttempts { get; set; }
    public DateTime? LockoutEnd { get; set; }
}
