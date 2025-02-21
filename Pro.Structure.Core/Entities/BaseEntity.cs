/*
 * File: BaseEntity.cs
 * Purpose: Defines the base abstract class for all entities in the system
 * Location: Pro.Structure.Core/Entities
 * 
 * This file contains the base entity class that all domain entities inherit from.
 * It provides common properties that are required across all entities in the system.
 */

namespace Pro.Structure.Core.Entities;

/// <summary>
/// Abstract base class that serves as the foundation for all entities in the system.
/// Provides common properties for entity identification and auditing.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Primary key identifier for the entity
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Timestamp indicating when the entity was first created
    /// Automatically set to current time when a new entity is instantiated
    /// </summary>
    public DateTime Created { get; set; } = DateTime.Now;

    /// <summary>
    /// Timestamp indicating when the entity was last modified
    /// Automatically set to current time when a new entity is instantiated
    /// Should be updated whenever the entity is modified
    /// </summary>
    public DateTime Modified { get; set; } = DateTime.Now;
}
