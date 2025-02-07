using System.ComponentModel.DataAnnotations;

namespace Pro.Structure.Web.ViewModels;

public class StatusViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Display(Name = "Number of Projects")]
    public int ProjectCount { get; set; }

    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
