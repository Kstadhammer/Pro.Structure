using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Core.Interfaces;

public interface ICustomerService : IBaseService<Customer>
{
    Task<ServiceResponse<CustomerModel>> GetByEmailAsync(string email);
    Task<ServiceResponse<bool>> ExistsByEmailAsync(string email);
    Task<ServiceResponse<IEnumerable<ProjectModel>>> GetCustomerProjectsAsync(int customerId);
    new Task<ServiceResponse<IEnumerable<CustomerModel>>> GetAllAsync();
    new Task<ServiceResponse<CustomerModel>> GetByIdAsync(int id);
}
