using Pro.Structure.Core.Entities;

namespace Pro.Structure.Core.Interfaces;

public interface IProjectManagerRepository : IBaseRepository<ProjectManager>
{
    Task<ProjectManager?> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
    Task<IEnumerable<ProjectManager>> GetAvailableManagersAsync();
}
