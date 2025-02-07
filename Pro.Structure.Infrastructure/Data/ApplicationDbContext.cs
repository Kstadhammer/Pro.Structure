using Microsoft.EntityFrameworkCore;
using Pro.Structure.Core.Entities;

namespace Pro.Structure.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;
    public DbSet<ProjectManager> ProjectManagers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Project configurations
        modelBuilder.Entity<Project>().Property(p => p.ProjectNumber).IsRequired();

        modelBuilder.Entity<Project>().Property(p => p.Name).IsRequired().HasMaxLength(100);

        // Customer configurations
        modelBuilder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(100);

        modelBuilder.Entity<Customer>().Property(c => c.Email).IsRequired();

        // Status configurations
        modelBuilder.Entity<Status>().Property(s => s.Name).IsRequired().HasMaxLength(50);

        // ProjectManager configurations
        modelBuilder.Entity<ProjectManager>().Property(pm => pm.Email).IsRequired();
    }
}
