namespace Pro.Structure.Core.Entities;

public class ProjectManager : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    // Navigation property
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
