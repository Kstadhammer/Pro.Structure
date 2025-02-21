/*
 * File: ProjectManager.cs
 * Purpose: Defines the ProjectManager entity representing project leaders in the system
 * Location: Pro.Structure.Core/Entities
 * 
 * This entity represents a project manager who oversees projects in the system.
 * It inherits from BaseEntity and maintains personal information and project relationships.
 */

namespace Pro.Structure.Core.Entities;

/// <summary>
/// Represents a project manager entity in the system.
/// Inherits basic properties (Id, Created, Modified) from BaseEntity.
/// Contains project manager's personal information and their associated projects.
/// </summary>
public class ProjectManager : BaseEntity
{
    /// <summary>
    /// The project manager's first name
    /// Required field, cannot be null
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// The project manager's last name
    /// Required field, cannot be null
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// The project manager's email address
    /// Required field, cannot be null
    /// Used for system communications and notifications
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// The project manager's contact phone number
    /// Required field, cannot be null
    /// Used for direct communication
    /// </summary>
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    /// Navigation property representing all projects managed by this project manager
    /// Implements a one-to-many relationship between ProjectManager and Project entities
    /// Initialized as an empty list to prevent null reference exceptions
    /// </summary>
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
