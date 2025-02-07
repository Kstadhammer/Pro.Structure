namespace Pro.Structure.Core.Entities;

public class Project : BaseEntity
{
    public string ProjectNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal TotalPrice { get; set; }

    // Navigation properties
    public int ProjectManagerId { get; set; }
    public ProjectManager ProjectManager { get; set; } = null!;

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public int StatusId { get; set; }
    public Status Status { get; set; } = null!;
}
