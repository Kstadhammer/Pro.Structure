using Pro.Structure.Core.Entities;

namespace Pro.Structure.Core.Interfaces;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<string> GenerateProjectNumberAsync();
    Task<IEnumerable<Project>> GetProjectsByCustomerAsync(int customerId);
    Task<IEnumerable<Project>> GetProjectsByStatusAsync(int statusId);
    Task<IEnumerable<Project>> GetProjectsByManagerAsync(int projectManagerId);
}
