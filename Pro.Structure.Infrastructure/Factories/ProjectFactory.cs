using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Factories;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Infrastructure.Factories;

public class ProjectFactory : IFactory<Project, ProjectModel>
{
    public Project CreateEntity(ProjectModel model)
    {
        return new Project
        {
            Id = model.Id,
            ProjectNumber = model.ProjectNumber,
            Name = model.Name,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            HourlyRate = model.HourlyRate,
            TotalPrice = model.TotalPrice,
            ProjectManagerId = model.ProjectManagerId,
            CustomerId = model.CustomerId,
            StatusId = model.StatusId,
            Created = model.Created,
            Modified = DateTime.Now,
        };
    }

    public ProjectModel CreateModel(Project entity)
    {
        return new ProjectModel
        {
            Id = entity.Id,
            ProjectNumber = entity.ProjectNumber,
            Name = entity.Name,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            HourlyRate = entity.HourlyRate,
            TotalPrice = entity.TotalPrice,
            ProjectManagerId = entity.ProjectManagerId,
            ProjectManagerName =
                $"{entity.ProjectManager.FirstName} {entity.ProjectManager.LastName}",
            CustomerId = entity.CustomerId,
            CustomerName = entity.Customer.Name,
            StatusId = entity.StatusId,
            StatusName = entity.Status.Name,
            Created = entity.Created,
            Modified = entity.Modified,
        };
    }
}
