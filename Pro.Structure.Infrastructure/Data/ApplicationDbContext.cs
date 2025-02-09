using Microsoft.EntityFrameworkCore;
using Pro.Structure.Core.Entities;

namespace Pro.Structure.Infrastructure.Data;

/// <summary>
/// Main database context for the application.
/// Handles all database operations and entity configurations.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    // Database tables
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;
    public DbSet<ProjectManager> ProjectManagers { get; set; } = null!;

    /// <summary>
    /// Configures the database model and relationships between entities.
    /// Sets up required fields, field lengths, and other constraints.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Project configurations - ProjectNumber is required and unique
        modelBuilder.Entity<Project>().Property(p => p.ProjectNumber).IsRequired();
        modelBuilder.Entity<Project>().Property(p => p.Name).IsRequired().HasMaxLength(100);

        // Customer configurations - Name and Email are required
        modelBuilder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Customer>().Property(c => c.Email).IsRequired();

        // Status configurations - Name is required with max length
        modelBuilder.Entity<Status>().Property(s => s.Name).IsRequired().HasMaxLength(50);

        // ProjectManager configurations - Email is required for notifications
        modelBuilder.Entity<ProjectManager>().Property(pm => pm.Email).IsRequired();
    }
}
