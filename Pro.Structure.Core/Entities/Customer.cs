namespace Pro.Structure.Core.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    // Navigation property
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
