using Microsoft.EntityFrameworkCore;
using Pro.Structure.Core.Entities;

namespace Pro.Structure.Infrastructure.Data;

public static class ApplicationDbContextSeed
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        try
        {
            // Only apply migrations if we're not in memory
            if (context.Database.IsSqlite())
            {
                await context.Database.MigrateAsync();
            }

            // Seed Statuses
            if (!await context.Statuses.AnyAsync())
            {
                await context.Statuses.AddRangeAsync(GetPreconfiguredStatuses());
                await context.SaveChangesAsync();
            }

            // Seed Project Managers
            if (!await context.ProjectManagers.AnyAsync())
            {
                await context.ProjectManagers.AddRangeAsync(GetPreconfiguredProjectManagers());
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error seeding database: {ex.Message}", ex);
        }
    }

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

    private static IEnumerable<ProjectManager> GetPreconfiguredProjectManagers()
    {
        return new List<ProjectManager>
        {
            new ProjectManager
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@prostructure.com",
                PhoneNumber = "+1234567890",
            },
            new ProjectManager
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@prostructure.com",
                PhoneNumber = "+1234567891",
            },
        };
    }
}
