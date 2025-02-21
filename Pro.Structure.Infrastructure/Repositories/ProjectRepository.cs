using Microsoft.EntityFrameworkCore;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Infrastructure.Data;

namespace Pro.Structure.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Project entity.
/// 
/// This implementation was developed with AI assistance for:
/// - Complex entity relationships and eager loading
/// - Project number generation logic
/// - Query optimization for filtered searches
/// - Multi-entity join operations
/// </summary>
public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext context)
        : base(context) { }

    /// <summary>
    /// Generates a unique project number for new projects.
    /// Implementation assisted by AI for sequential number
    /// generation and string manipulation.
    /// </summary>
    public async Task<string> GenerateProjectNumberAsync()
    {
        var lastProject = await _dbSet
            .OrderByDescending(p => p.ProjectNumber)
            .FirstOrDefaultAsync();

        if (lastProject == null)
        {
            return "P-101";
        }

        var currentNumber = int.Parse(lastProject.ProjectNumber.Split('-')[1]);
        return $"P-{currentNumber + 1}";
    }

    /// <summary>
    /// Retrieves all projects for a specific customer.
    /// Implementation assisted by AI for proper eager loading
    /// of related entities and query optimization.
    /// </summary>
    public async Task<IEnumerable<Project>> GetProjectsByCustomerAsync(int customerId)
    {
        return await _dbSet
            .Include(p => p.Customer)
            .Include(p => p.Status)
            .Include(p => p.ProjectManager)
            .Where(p => p.CustomerId == customerId)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves all projects with a specific status.
    /// Implementation assisted by AI for proper eager loading
    /// and filtered query optimization.
    /// </summary>
    public async Task<IEnumerable<Project>> GetProjectsByStatusAsync(int statusId)
    {
        return await _dbSet
            .Include(p => p.Customer)
            .Include(p => p.Status)
            .Include(p => p.ProjectManager)
            .Where(p => p.StatusId == statusId)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves all projects for a specific project manager.
    /// Implementation assisted by AI for proper eager loading
    /// and filtered query optimization.
    /// </summary>
    public async Task<IEnumerable<Project>> GetProjectsByManagerAsync(int projectManagerId)
    {
        return await _dbSet
            .Include(p => p.Customer)
            .Include(p => p.Status)
            .Include(p => p.ProjectManager)
            .Where(p => p.ProjectManagerId == projectManagerId)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves all projects with their related entities.
    /// Implementation assisted by AI for proper eager loading
    /// of multiple related entities.
    /// </summary>
    public override async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _dbSet
            .Include(p => p.Customer)
            .Include(p => p.Status)
            .Include(p => p.ProjectManager)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a project by ID with all related entities.
    /// Implementation assisted by AI for proper eager loading
    /// and null handling.
    /// </summary>
    public override async Task<Project?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Customer)
            .Include(p => p.Status)
            .Include(p => p.ProjectManager)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
