namespace Pro.Structure.Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Modified { get; set; } = DateTime.Now;
}
