/*
 * File: Project.cs
 * Purpose: Defines the Project entity which is a core business object in the system
 * Location: Pro.Structure.Core/Entities
 * 
 * This entity represents a project in the system and inherits from BaseEntity.
 * It maintains relationships with ProjectManager, Customer, and Status entities
 * while storing project-specific information such as timing and pricing details.
 */

namespace Pro.Structure.Core.Entities;

/// <summary>
/// Represents a project entity in the system.
/// Inherits basic properties (Id, Created, Modified) from BaseEntity.
/// Contains project-specific information and relationships with other entities.
/// </summary>
public class Project : BaseEntity
{
    /// <summary>
    /// Unique identifier for the project, typically a business-specific format
    /// Required field, cannot be null
    /// </summary>
    public string ProjectNumber { get; set; } = null!;

    /// <summary>
    /// The name/title of the project
    /// Required field, cannot be null
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The date when the project is scheduled to begin
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// The date when the project is scheduled to complete
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// The rate charged per hour for the project work
    /// Stored as decimal to ensure precise financial calculations
    /// </summary>
    public decimal HourlyRate { get; set; }

    /// <summary>
    /// The total price for the entire project
    /// Stored as decimal to ensure precise financial calculations
    /// </summary>
    public decimal TotalPrice { get; set; }

    // Navigation properties and foreign keys

    /// <summary>
    /// Foreign key for the ProjectManager relationship
    /// </summary>
    public int ProjectManagerId { get; set; }

    /// <summary>
    /// Navigation property to the associated ProjectManager
    /// Required relationship, cannot be null
    /// </summary>
    public ProjectManager ProjectManager { get; set; } = null!;

    /// <summary>
    /// Foreign key for the Customer relationship
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Navigation property to the associated Customer
    /// Required relationship, cannot be null
    /// </summary>
    public Customer Customer { get; set; } = null!;

    /// <summary>
    /// Foreign key for the Status relationship
    /// </summary>
    public int StatusId { get; set; }

    /// <summary>
    /// Navigation property to the associated Status
    /// Required relationship, cannot be null
    /// Represents the current state of the project
    /// </summary>
    public Status Status { get; set; } = null!;
}
