using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Factories;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Infrastructure.Factories;

/// <summary>
/// Factory for converting between Project entities and ProjectModel DTOs.
/// 
/// This implementation was developed with AI assistance for:
/// - Proper mapping of complex relationships
/// - Handling of navigation properties
/// - Date/time management
/// - Composite field generation
/// </summary>
public class ProjectFactory : IFactory<Project, ProjectModel>
{
    /// <summary>
    /// Creates a Project entity from a ProjectModel.
    /// Implementation assisted by AI for proper property mapping
    /// and timestamp management.
    /// </summary>
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

    /// <summary>
    /// Creates a ProjectModel from a Project entity.
    /// Implementation assisted by AI for handling navigation properties
    /// and creating composite fields like ProjectManagerName.
    /// </summary>
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
