using Pro.Structure.Core.Entities;

namespace Pro.Structure.Core.Interfaces;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    Task<Customer?> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
}
