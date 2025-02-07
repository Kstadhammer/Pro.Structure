using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Factories;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Infrastructure.Factories;

public class StatusFactory : IFactory<Status, StatusModel>
{
    public Status CreateEntity(StatusModel model)
    {
        return new Status
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Created = model.Created,
            Modified = DateTime.Now,
        };
    }

    public StatusModel CreateModel(Status entity)
    {
        return new StatusModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            ProjectCount = entity.Projects.Count,
            Created = entity.Created,
            Modified = entity.Modified,
        };
    }
}
