namespace Pro.Structure.Core.Entities;

public class Status : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    // Navigation property
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
