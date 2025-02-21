/*
 * File: Status.cs
 * Purpose: Defines the Status entity representing project states in the system
 * Location: Pro.Structure.Core/Entities
 * 
 * This entity represents different states that a project can be in (e.g., "In Progress", "Completed").
 * It inherits from BaseEntity and maintains a relationship with Project entities.
 */

namespace Pro.Structure.Core.Entities;

/// <summary>
/// Represents a status entity in the system.
/// Inherits basic properties (Id, Created, Modified) from BaseEntity.
/// Used to track and categorize the current state of projects.
/// </summary>
public class Status : BaseEntity
{
    /// <summary>
    /// The name of the status (e.g., "In Progress", "Completed", "On Hold")
    /// Required field, cannot be null
    /// Should be unique and descriptive
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// A detailed description of what this status represents
    /// Required field, cannot be null
    /// Provides additional context about the status meaning
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Navigation property representing all projects currently in this status
    /// Implements a one-to-many relationship between Status and Project entities
    /// Initialized as an empty list to prevent null reference exceptions
    /// </summary>
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
