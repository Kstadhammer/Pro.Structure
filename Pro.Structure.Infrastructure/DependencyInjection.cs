using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
/// </summary>
public static class DependencyInjection
{
    public static async Task<IServiceCollection> AddInfrastructureAsync(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // Configure SQLite database with migrations support
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            )
        );

        // Add transaction management support
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Initialize database and apply migrations
        var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await ApplicationDbContextSeed.SeedAsync(context);

        // Register repositories - each handles database operations for a specific entity
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<IProjectManagerRepository, ProjectManagerRepository>();

        // Register factories - handle conversion between entities and models
        services.AddScoped<IFactory<Project, ProjectModel>, ProjectFactory>();
        services.AddScoped<IFactory<Customer, CustomerModel>, CustomerFactory>();
        services.AddScoped<IFactory<Status, StatusModel>, StatusFactory>();
        services.AddScoped<IFactory<ProjectManager, ProjectManagerModel>, ProjectManagerFactory>();

        // Register services - implement business logic and orchestrate operations
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IProjectManagerService, ProjectManagerService>();

        return services;
    }
}
