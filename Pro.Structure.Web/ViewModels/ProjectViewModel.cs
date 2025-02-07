using System.ComponentModel.DataAnnotations;

namespace Pro.Structure.Web.ViewModels;

public class ProjectViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Project Number")]
    public string ProjectNumber { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [Display(Name = "Start Date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [Display(Name = "End Date")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    [Required]
    [Display(Name = "Hourly Rate")]
    [DataType(DataType.Currency)]
    public decimal HourlyRate { get; set; }

    [Required]
    [Display(Name = "Total Price")]
    [DataType(DataType.Currency)]
    public decimal TotalPrice { get; set; }

    [Required]
    [Display(Name = "Project Manager")]
    public int ProjectManagerId { get; set; }
    public string ProjectManagerName { get; set; } = null!;

    [Required]
    [Display(Name = "Customer")]
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;

    [Required]
    [Display(Name = "Status")]
    public int StatusId { get; set; }
    public string StatusName { get; set; } = null!;

    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
