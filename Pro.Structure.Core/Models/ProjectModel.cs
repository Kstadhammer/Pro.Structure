namespace Pro.Structure.Core.Models;

public class ProjectModel
{
    public int Id { get; set; }
    public string ProjectNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal TotalPrice { get; set; }
    public int ProjectManagerId { get; set; }
    public string ProjectManagerName { get; set; } = null!;
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public int StatusId { get; set; }
    public string StatusName { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
