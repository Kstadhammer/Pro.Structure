using System.ComponentModel.DataAnnotations;

namespace Pro.Structure.Web.ViewModels;

public class ProfileViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;

    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    [Phone(ErrorMessage = "Invalid phone number")]
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Current Password")]
    [DataType(DataType.Password)]
    public string? CurrentPassword { get; set; }

    [Display(Name = "New Password")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
    public string? NewPassword { get; set; }

    [Display(Name = "Confirm New Password")]
    [DataType(DataType.Password)]
    [Compare(
        "NewPassword",
        ErrorMessage = "The new password and confirmation password do not match"
    )]
    public string? ConfirmNewPassword { get; set; }

    public DateTime Created { get; set; }
    public DateTime? LastLogin { get; set; }
    public string Role { get; set; } = string.Empty;
}
