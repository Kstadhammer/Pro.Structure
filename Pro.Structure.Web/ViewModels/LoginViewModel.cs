using System.ComponentModel.DataAnnotations;

namespace Pro.Structure.Web.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email or username is required")]
    [Display(Name = "Email or Username")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}
