using System.ComponentModel.DataAnnotations;

namespace Pro.Structure.Web.ViewModels;

public class ForgotPasswordViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;
}
