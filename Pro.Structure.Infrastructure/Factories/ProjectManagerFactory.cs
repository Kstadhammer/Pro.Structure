using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Factories;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Infrastructure.Factories;

public class ProjectManagerFactory : IFactory<ProjectManager, ProjectManagerModel>
{
    public ProjectManager CreateEntity(ProjectManagerModel model)
    {
        return new ProjectManager
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Created = model.Created,
            Modified = DateTime.Now,
        };
    }

    public ProjectManagerModel CreateModel(ProjectManager entity)
    {
        return new ProjectManagerModel
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            ProjectCount = entity.Projects.Count,
            Created = entity.Created,
            Modified = entity.Modified,
        };
    }
}
