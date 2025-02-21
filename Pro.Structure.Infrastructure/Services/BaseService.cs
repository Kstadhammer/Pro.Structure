using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Infrastructure.Services;

/// <summary>
/// Base service implementation that provides common CRUD operations for all entities.
/// This implementation was developed with AI assistance for error handling patterns
/// and ServiceResponse integration.
/// </summary>
public class BaseService<T> : IBaseService<T>
    where T : BaseEntity
{
    protected readonly IBaseRepository<T> _repository;

    public BaseService(IBaseRepository<T> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Retrieves all entities of type T.
    /// Implementation assisted by AI for proper error handling and response formatting.
    /// </summary>
    public virtual async Task<ServiceResponse<IEnumerable<T>>> GetAllAsync()
    {
        try
        {
            var entities = await _repository.GetAllAsync();
            return ServiceResponse<IEnumerable<T>>.Ok(entities);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<T>>.Fail($"Error retrieving entities: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a single entity by its identifier.
    /// Implementation assisted by AI for proper error handling and null checking.
    /// </summary>
    public virtual async Task<ServiceResponse<T>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResponse<T>.Fail("Entity not found");

            return ServiceResponse<T>.Ok(entity);
        }
        catch (Exception ex)
        {
            return ServiceResponse<T>.Fail($"Error retrieving entity: {ex.Message}");
        }
    }

    /// <summary>
    /// Adds a new entity to the system.
    /// Implementation assisted by AI for proper error handling and success verification.
    /// </summary>
    public virtual async Task<ServiceResponse<T>> AddAsync(T entity)
    {
        try
        {
            var result = await _repository.AddAsync(entity);
            if (!result)
                return ServiceResponse<T>.Fail("Failed to add entity");

            return ServiceResponse<T>.Ok(entity, "Entity added successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<T>.Fail($"Error adding entity: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing entity in the system.
    /// Implementation assisted by AI for proper error handling and success verification.
    /// </summary>
    public virtual async Task<ServiceResponse<T>> UpdateAsync(T entity)
    {
        try
        {
            var result = await _repository.UpdateAsync(entity);
            if (!result)
                return ServiceResponse<T>.Fail("Failed to update entity");

            return ServiceResponse<T>.Ok(entity, "Entity updated successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<T>.Fail($"Error updating entity: {ex.Message}");
        }
    }

    /// <summary>
    /// Deletes an entity from the system by its identifier.
    /// Implementation assisted by AI for proper error handling and success verification.
    /// </summary>
    public virtual async Task<ServiceResponse<bool>> DeleteAsync(int id)
    {
        try
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
                return ServiceResponse<bool>.Fail("Failed to delete entity");

            return ServiceResponse<bool>.Ok(true, "Entity deleted successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<bool>.Fail($"Error deleting entity: {ex.Message}");
        }
    }

    /// <summary>
    /// Checks if an entity with the specified identifier exists in the system.
    /// Implementation assisted by AI for proper error handling.
    /// </summary>
    public virtual async Task<ServiceResponse<bool>> ExistsAsync(int id)
    {
        try
        {
            var exists = await _repository.ExistsAsync(id);
            return ServiceResponse<bool>.Ok(exists);
        }
        catch (Exception ex)
        {
            return ServiceResponse<bool>.Fail($"Error checking entity existence: {ex.Message}");
        }
    }
}
