/*
 * File: User.cs
 * Purpose: Defines the User entity for authentication and authorization in the system
 * Location: Pro.Structure.Core/Entities
 * 
 * This entity represents a user account in the system with authentication and authorization details.
 * It inherits from BaseEntity and includes security features like password hashing and account lockout.
 */

using System.ComponentModel.DataAnnotations;

namespace Pro.Structure.Core.Entities;

/// <summary>
/// Represents a user account in the system.
/// Inherits basic properties (Id, Created, Modified) from BaseEntity.
/// Includes authentication, authorization, and security features.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// The user's email address, used for authentication and communication
    /// Required field, validated as a proper email address format
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's chosen username for the system
    /// Required field, must be unique in the system
    /// </summary>
    [Required]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The hashed password for the user's account
    /// Required field, should never store plain text passwords
    /// </summary>
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// The user's first name
    /// Optional field, can be null
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// The user's last name
    /// Optional field, can be null
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// The user's contact phone number
    /// Optional field, can be null
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// The user's role in the system for authorization purposes
    /// Defaults to "User" role
    /// Used for role-based access control
    /// </summary>
    public string Role { get; set; } = "User";

    /// <summary>
    /// Indicates if the user account is currently active
    /// Defaults to true
    /// Can be used to disable accounts without deleting them
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Timestamp of the user's last successful login
    /// Nullable, tracks user activity
    /// </summary>
    public DateTime? LastLogin { get; set; }

    /// <summary>
    /// Counter for failed login attempts
    /// Used for security monitoring and account lockout
    /// </summary>
    public int FailedLoginAttempts { get; set; }

    /// <summary>
    /// Timestamp until which the account is locked
    /// Nullable, used for temporary account lockouts after multiple failed login attempts
    /// </summary>
    public DateTime? LockoutEnd { get; set; }
}
