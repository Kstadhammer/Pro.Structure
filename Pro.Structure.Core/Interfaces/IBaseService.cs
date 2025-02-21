/*
 * File: IBaseService.cs
 * Purpose: Defines the base service interface for business logic operations
 * Location: Pro.Structure.Core/Interfaces
 * 
 * This interface provides a contract for basic service operations that wrap repository calls
 * with additional business logic and error handling. It ensures consistent service behavior
 * across different entity types and returns standardized ServiceResponse objects.
 */

using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Core.Interfaces;

/// <summary>
/// Generic base service interface that defines standard business operations.
/// Wraps repository operations with additional business logic and error handling.
/// </summary>
/// <typeparam name="T">The entity type, must inherit from BaseEntity</typeparam>
public interface IBaseService<T>
    where T : BaseEntity
{
    /// <summary>
    /// Retrieves all entities of type T
    /// </summary>
    /// <returns>A ServiceResponse containing an enumerable collection of all entities if successful,
    /// or error details if the operation fails</returns>
    Task<ServiceResponse<IEnumerable<T>>> GetAllAsync();

    /// <summary>
    /// Retrieves a single entity by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <returns>A ServiceResponse containing the entity if found and operation is successful,
    /// or error details if the operation fails or entity is not found</returns>
    Task<ServiceResponse<T>> GetByIdAsync(int id);

    /// <summary>
    /// Adds a new entity to the system
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>A ServiceResponse containing the newly created entity if successful,
    /// or error details if the operation fails</returns>
    Task<ServiceResponse<T>> AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity in the system
    /// </summary>
    /// <param name="entity">The entity with updated values</param>
    /// <returns>A ServiceResponse containing the updated entity if successful,
    /// or error details if the operation fails</returns>
    Task<ServiceResponse<T>> UpdateAsync(T entity);

    /// <summary>
    /// Deletes an entity from the system by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete</param>
    /// <returns>A ServiceResponse containing true if the deletion was successful,
    /// or error details if the operation fails</returns>
    Task<ServiceResponse<bool>> DeleteAsync(int id);

    /// <summary>
    /// Checks if an entity with the specified identifier exists in the system
    /// </summary>
    /// <param name="id">The unique identifier to check</param>
    /// <returns>A ServiceResponse containing true if the entity exists,
    /// false if it doesn't, or error details if the operation fails</returns>
    Task<ServiceResponse<bool>> ExistsAsync(int id);
}
