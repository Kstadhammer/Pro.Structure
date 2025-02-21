/*
 * File: Customer.cs
 * Purpose: Defines the Customer entity representing clients in the system
 * Location: Pro.Structure.Core/Entities
 * 
 * This entity represents a customer/client in the system and inherits from BaseEntity.
 * It contains essential customer information and maintains a relationship with projects.
 */

namespace Pro.Structure.Core.Entities;

/// <summary>
/// Represents a customer entity in the system.
/// Inherits basic properties (Id, Created, Modified) from BaseEntity.
/// Contains customer-specific information and relationships.
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// The full name of the customer
    /// Required field, cannot be null
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The customer's email address for communication
    /// Required field, cannot be null
    /// Used for notifications and correspondence
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// The customer's contact phone number
    /// Required field, cannot be null
    /// Used for direct communication
    /// </summary>
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    /// Navigation property representing all projects associated with this customer
    /// Implements a one-to-many relationship between Customer and Project entities
    /// Initialized as an empty list to prevent null reference exceptions
    /// </summary>
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
