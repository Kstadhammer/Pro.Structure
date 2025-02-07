namespace Pro.Structure.Core.Models;

public class ProjectManagerModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public int ProjectCount { get; set; }
    public bool IsAvailable => ProjectCount < 5;
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
