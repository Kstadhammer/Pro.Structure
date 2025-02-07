using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Core.Interfaces;

public interface IProjectManagerService : IBaseService<ProjectManager>
{
    Task<ServiceResponse<ProjectManagerModel>> GetByEmailAsync(string email);
    Task<ServiceResponse<bool>> ExistsByEmailAsync(string email);
    Task<ServiceResponse<IEnumerable<ProjectManagerModel>>> GetAvailableManagersAsync();
    Task<ServiceResponse<IEnumerable<ProjectModel>>> GetManagerProjectsAsync(int managerId);
    new Task<ServiceResponse<IEnumerable<ProjectManagerModel>>> GetAllAsync();
    new Task<ServiceResponse<ProjectManagerModel>> GetByIdAsync(int id);
}
