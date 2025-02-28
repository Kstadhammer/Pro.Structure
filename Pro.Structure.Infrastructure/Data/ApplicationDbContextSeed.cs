using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pro.Structure.Core.Entities;

namespace Pro.Structure.Infrastructure.Data;

/// <summary>
/// Handles database seeding operations for the application.
///
/// This implementation was developed with AI assistance for:
/// - Database migration management
/// - Secure password hashing
/// - Data consistency checks
/// - Error handling and logging
/// - Initial data seeding strategy
/// </summary>
public static class ApplicationDbContextSeed
{
    /// <summary>
    /// Seeds the database with initial data.
    /// Implementation assisted by AI for proper transaction handling
    /// and data validation.
    /// </summary>
    public static async Task SeedAsync(ApplicationDbContext context, ILogger logger)
    {
        try
        {
            // Only apply migrations if we're not in memory
            if (context.Database.IsSqlite())
            {
                logger.LogInformation("Applying migrations...");
                await context.Database.MigrateAsync();
            }

            // Seed Statuses
            if (!await context.Statuses.AnyAsync())
            {
                logger.LogInformation("Seeding statuses...");
                var statuses = GetPreconfiguredStatuses();
                await context.Statuses.AddRangeAsync(statuses);
                await context.SaveChangesAsync();
                logger.LogInformation("Successfully seeded {Count} statuses", statuses.Count());
            }
            else
            {
                logger.LogInformation("Statuses already exist in database");
            }

            // Seed Project Managers
            if (!await context.ProjectManagers.AnyAsync())
            {
                logger.LogInformation("Seeding project managers...");
                var managers = GetPreconfiguredProjectManagers();
                await context.ProjectManagers.AddRangeAsync(managers);
                await context.SaveChangesAsync();
                logger.LogInformation(
                    "Successfully seeded {Count} project managers",
                    managers.Count()
                );
            }
            else
            {
                logger.LogInformation("Project managers already exist in database");
            }

            // Seed Customers
            if (!await context.Customers.AnyAsync())
            {
                logger.LogInformation("Seeding customers...");
                var customers = GetPreconfiguredCustomers();
                await context.Customers.AddRangeAsync(customers);
                await context.SaveChangesAsync();
                logger.LogInformation("Successfully seeded {Count} customers", customers.Count());
            }
            else
            {
                logger.LogInformation("Customers already exist in database");
            }

            // Seed Admin User
            if (!await context.Users.AnyAsync())
            {
                logger.LogInformation("Seeding admin user...");
                var adminUser = new User
                {
                    Email = "admin@prostructure.com",
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                    FirstName = "Admin",
                    LastName = "User",
                    Role = "Admin",
                    IsActive = true,
                    Created = DateTime.UtcNow,
                    Modified = DateTime.UtcNow,
                };
                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();
                logger.LogInformation("Successfully seeded admin user");
            }
            else
            {
                logger.LogInformation("Users already exist in database");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error seeding database");
            throw new Exception($"Error seeding database: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Provides preconfigured status entities.
    /// Implementation assisted by AI for comprehensive
    /// status definitions and descriptions.
    /// </summary>
    private static IEnumerable<Status> GetPreconfiguredStatuses()
    {
        return new List<Status>
        {
            new Status
            {
                Name = "Not Started",
                Description = "Project has been created but work has not begun",
            },
            new Status
            {
                Name = "In Progress",
                Description = "Project is currently being worked on",
            },
            new Status { Name = "On Hold", Description = "Project has been temporarily paused" },
            new Status { Name = "Completed", Description = "Project has been finished" },
            new Status
            {
                Name = "Cancelled",
                Description = "Project has been cancelled before completion",
            },
        };
    }

    /// <summary>
    /// Provides preconfigured project manager entities.
    /// Implementation assisted by AI for data structure
    /// and validation patterns.
    /// </summary>
    private static IEnumerable<ProjectManager> GetPreconfiguredProjectManagers()
    {
        return new List<ProjectManager>
        {
            new ProjectManager
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+1234567890",
            },
            new ProjectManager
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                PhoneNumber = "+1234567891",
            },
        };
    }

    /// <summary>
    /// Provides preconfigured customer entities.
    /// Implementation assisted by AI for data structure
    /// and validation patterns.
    /// </summary>
    private static IEnumerable<Customer> GetPreconfiguredCustomers()
    {
        return new List<Customer>
        {
            new Customer
            {
                Name = "Alice Johnson",
                Email = "alice.johnson@customer.com",
                PhoneNumber = "+1234567892",
            },
            new Customer
            {
                Name = "Bob Anderson",
                Email = "bob.anderson@customer.com",
                PhoneNumber = "+1234567893",
            },
        };
    }
}
