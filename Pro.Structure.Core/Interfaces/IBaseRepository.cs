/*
 * File: IBaseRepository.cs
 * Purpose: Defines the base repository interface for data access operations
 * Location: Pro.Structure.Core/Interfaces
 *
 * This interface provides a contract for basic CRUD (Create, Read, Update, Delete) operations
 * that all repositories in the system must implement. It ensures consistent data access patterns
 * across different entity types.
 */

using System.Linq.Expressions;
using Pro.Structure.Core.Entities;

namespace Pro.Structure.Core.Interfaces;

/// <summary>
/// Generic base repository interface that defines standard CRUD operations.
/// </summary>
/// <typeparam name="T">The entity type, must inherit from BaseEntity</typeparam>
public interface IBaseRepository<T>
    where T : BaseEntity
{
    /// <summary>
    /// Retrieves all entities of type T from the repository
    /// </summary>
    /// <returns>An enumerable collection of all entities</returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Retrieves a single entity by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <returns>The entity if found, null otherwise</returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves a single entity based on a predicate expression
    /// </summary>
    /// <param name="expression">The predicate expression to filter by</param>
    /// <returns>The entity if found, null otherwise</returns>
    Task<T?> GetAsync(Expression<Func<T, bool>> expression);

    /// <summary>
    /// Adds a new entity to the repository
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>True if the operation was successful, false otherwise</returns>
    Task<bool> AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity in the repository
    /// </summary>
    /// <param name="entity">The entity with updated values</param>
    /// <returns>True if the operation was successful, false otherwise</returns>
    Task<bool> UpdateAsync(T entity);

    /// <summary>
    /// Deletes an entity from the repository by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete</param>
    /// <returns>True if the operation was successful, false otherwise</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Checks if an entity with the specified identifier exists in the repository
    /// </summary>
    /// <param name="id">The unique identifier to check</param>
    /// <returns>True if the entity exists, false otherwise</returns>
    Task<bool> ExistsAsync(int id);
}
