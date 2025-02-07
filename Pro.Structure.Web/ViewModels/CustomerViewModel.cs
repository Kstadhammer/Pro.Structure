using System.ComponentModel.DataAnnotations;

namespace Pro.Structure.Web.ViewModels;

public class CustomerViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Phone]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = null!;

    [Display(Name = "Number of Projects")]
    public int ProjectCount { get; set; }

    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
