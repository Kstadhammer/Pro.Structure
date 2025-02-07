using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Core.Interfaces;

public interface IProjectService : IBaseService<Project>
{
    Task<ServiceResponse<string>> GenerateProjectNumberAsync();
    Task<ServiceResponse<IEnumerable<ProjectModel>>> GetProjectsByCustomerAsync(int customerId);
    Task<ServiceResponse<IEnumerable<ProjectModel>>> GetProjectsByStatusAsync(int statusId);
    Task<ServiceResponse<IEnumerable<ProjectModel>>> GetProjectsByManagerAsync(
        int projectManagerId
    );
    Task<ServiceResponse<ProjectModel>> UpdateProjectStatusAsync(int projectId, int statusId);
    new Task<ServiceResponse<IEnumerable<ProjectModel>>> GetAllAsync();
    new Task<ServiceResponse<ProjectModel>> GetByIdAsync(int id);
}
