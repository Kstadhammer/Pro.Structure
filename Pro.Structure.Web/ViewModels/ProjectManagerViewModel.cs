using System.ComponentModel.DataAnnotations;

namespace Pro.Structure.Web.ViewModels;

public class ProjectManagerViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Full Name")]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Phone]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = null!;

    [Display(Name = "Active Projects")]
    public int ActiveProjectCount { get; set; }

    [Display(Name = "Total Projects")]
    public int TotalProjectCount { get; set; }

    [Display(Name = "Available for New Projects")]
    public bool IsAvailable { get; set; }

    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
