using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Factories;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;
using Pro.Structure.Infrastructure.Data;
using Pro.Structure.Infrastructure.Factories;
using Pro.Structure.Infrastructure.Repositories;
using Pro.Structure.Infrastructure.Services;

namespace Pro.Structure.Infrastructure;

/// <summary>
/// Configures dependency injection for the application.
/// Sets up database, repositories, services, and factories.
/// 
/// This implementation was developed with AI assistance for:
/// - Dependency injection configuration
/// - Service lifetime management
/// - Database initialization and seeding
/// - Repository and service registration patterns
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Configures and registers all infrastructure services.
    /// Implementation assisted by AI for proper service registration order
    /// and dependency management.
    /// </summary>
    public static async Task<IServiceCollection> AddInfrastructureAsync(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // Configure SQLite database with migrations support
        // AI assisted for proper migration configuration
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            )
        );

        // Add transaction management support
        // AI assisted for proper scoping and lifecycle management
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Initialize database and apply migrations
        // AI assisted for proper initialization sequence
        var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
        await ApplicationDbContextSeed.SeedAsync(context, logger);

        // Register repositories - each handles database operations for a specific entity
        // AI assisted for consistent repository registration pattern
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<IProjectManagerRepository, ProjectManagerRepository>();

        // Register factories - handle conversion between entities and models
        // AI assisted for proper factory pattern implementation
        services.AddScoped<IFactory<Project, ProjectModel>, ProjectFactory>();
        services.AddScoped<IFactory<Customer, CustomerModel>, CustomerFactory>();
        services.AddScoped<IFactory<Status, StatusModel>, StatusFactory>();
        services.AddScoped<IFactory<ProjectManager, ProjectManagerModel>, ProjectManagerFactory>();

        // Register services - implement business logic and orchestrate operations
        // AI assisted for proper service layer organization
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IProjectManagerService, ProjectManagerService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
