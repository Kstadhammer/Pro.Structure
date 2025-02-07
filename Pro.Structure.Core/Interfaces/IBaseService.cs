using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Core.Interfaces;

public interface IBaseService<T>
    where T : BaseEntity
{
    Task<ServiceResponse<IEnumerable<T>>> GetAllAsync();
    Task<ServiceResponse<T>> GetByIdAsync(int id);
    Task<ServiceResponse<T>> AddAsync(T entity);
    Task<ServiceResponse<T>> UpdateAsync(T entity);
    Task<ServiceResponse<bool>> DeleteAsync(int id);
    Task<ServiceResponse<bool>> ExistsAsync(int id);
}
