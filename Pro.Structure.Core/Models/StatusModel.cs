namespace Pro.Structure.Core.Models;

public class StatusModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ProjectCount { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
