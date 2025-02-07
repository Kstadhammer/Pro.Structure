using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Core.Interfaces;

public interface IStatusService : IBaseService<Status>
{
    Task<ServiceResponse<StatusModel>> GetByNameAsync(string name);
    Task<ServiceResponse<bool>> ExistsByNameAsync(string name);
    Task<ServiceResponse<IEnumerable<ProjectModel>>> GetProjectsByStatusAsync(int statusId);
    new Task<ServiceResponse<IEnumerable<StatusModel>>> GetAllAsync();
    new Task<ServiceResponse<StatusModel>> GetByIdAsync(int id);
}
