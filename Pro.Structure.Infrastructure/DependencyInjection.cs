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

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // Database
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            )
        );

        // Repositories
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<IProjectManagerRepository, ProjectManagerRepository>();

        // Factories
        services.AddScoped<IFactory<Project, ProjectModel>, ProjectFactory>();
        services.AddScoped<IFactory<Customer, CustomerModel>, CustomerFactory>();
        services.AddScoped<IFactory<Status, StatusModel>, StatusFactory>();
        services.AddScoped<IFactory<ProjectManager, ProjectManagerModel>, ProjectManagerFactory>();

        // Services
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IProjectManagerService, ProjectManagerService>();

        return services;
    }
}
