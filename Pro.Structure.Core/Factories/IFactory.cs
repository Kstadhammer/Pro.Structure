/*
 * File: IFactory.cs
 * Purpose: Defines the generic factory interface for converting between entity and model types
 * Location: Pro.Structure.Core/Factories
 * 
 * This interface provides a contract for factory classes that handle the conversion
 * between domain entities and their corresponding data transfer objects (DTOs/Models).
 */

namespace Pro.Structure.Core.Factories;

/// <summary>
/// Generic factory interface for converting between entity and model types.
/// </summary>
/// <typeparam name="TEntity">The domain entity type</typeparam>
/// <typeparam name="TModel">The model/DTO type</typeparam>
public interface IFactory<TEntity, TModel>
{
    /// <summary>
    /// Creates a domain entity from a model/DTO
    /// </summary>
    /// <param name="model">The model/DTO to convert from</param>
    /// <returns>A new domain entity instance</returns>
    TEntity CreateEntity(TModel model);

    /// <summary>
    /// Creates a model/DTO from a domain entity
    /// </summary>
    /// <param name="entity">The domain entity to convert from</param>
    /// <returns>A new model/DTO instance</returns>
    TModel CreateModel(TEntity entity);
}
