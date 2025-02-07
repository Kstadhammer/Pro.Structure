using System.ComponentModel.DataAnnotations;

namespace Pro.Structure.Web.ViewModels;

public class ProjectViewModel
{
    public int Id { get; set; }

    [Display(Name = "Project Number")]
    public string? ProjectNumber { get; set; }

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
    [Range(0, double.MaxValue, ErrorMessage = "Hourly Rate must be greater than or equal to 0")]
    public decimal HourlyRate { get; set; }

    [Required]
    [Display(Name = "Total Price")]
    [DataType(DataType.Currency)]
    [Range(0, double.MaxValue, ErrorMessage = "Total Price must be greater than or equal to 0")]
    public decimal TotalPrice { get; set; }

    [Required]
    [Display(Name = "Project Manager")]
    public int ProjectManagerId { get; set; }

    [Display(Name = "Project Manager")]
    public string? ProjectManagerName { get; set; }

    [Required]
    [Display(Name = "Customer")]
    public int CustomerId { get; set; }

    [Display(Name = "Customer")]
    public string? CustomerName { get; set; }

    [Required]
    [Display(Name = "Status")]
    public int StatusId { get; set; }

    [Display(Name = "Status")]
    public string? StatusName { get; set; }

    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
