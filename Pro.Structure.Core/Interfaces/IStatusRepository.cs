using Pro.Structure.Core.Entities;

namespace Pro.Structure.Core.Interfaces;

public interface IStatusRepository : IBaseRepository<Status>
{
    Task<Status?> GetByNameAsync(string name);
    Task<bool> ExistsByNameAsync(string name);
}
