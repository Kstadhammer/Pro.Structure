using Microsoft.EntityFrameworkCore;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Infrastructure.Data;

namespace Pro.Structure.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for ProjectManager entity.
/// 
/// This implementation was developed with AI assistance for:
/// - Efficient eager loading of related projects
/// - Email-based manager lookup and validation
/// - Workload management logic
/// - Query optimization patterns
/// </summary>
public class ProjectManagerRepository : BaseRepository<ProjectManager>, IProjectManagerRepository
{
    public ProjectManagerRepository(ApplicationDbContext context)
        : base(context) { }

    /// <summary>
    /// Retrieves a project manager by their email address.
    /// Implementation assisted by AI for proper eager loading
    /// of related projects.
    /// </summary>
    public async Task<ProjectManager?> GetByEmailAsync(string email)
    {
        return await _dbSet.Include(pm => pm.Projects).FirstOrDefaultAsync(pm => pm.Email == email);
    }

    /// <summary>
    /// Checks if a project manager with the given email exists.
    /// Implementation assisted by AI for efficient existence checking.
    /// </summary>
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _dbSet.AnyAsync(pm => pm.Email == email);
    }

    /// <summary>
    /// Retrieves project managers who have capacity for new projects.
    /// Implementation assisted by AI for workload calculation
    /// and filtered query optimization.
    /// </summary>
    public async Task<IEnumerable<ProjectManager>> GetAvailableManagersAsync()
    {
        return await _dbSet
            .Include(pm => pm.Projects)
            .Where(pm => pm.Projects.Count < 5) // Assuming a manager can handle up to 5 projects
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves all project managers with their related projects.
    /// Implementation assisted by AI for proper eager loading
    /// and query optimization.
    /// </summary>
    public override async Task<IEnumerable<ProjectManager>> GetAllAsync()
    {
        return await _dbSet.Include(pm => pm.Projects).ToListAsync();
    }

    /// <summary>
    /// Retrieves a project manager by ID with their related projects.
    /// Implementation assisted by AI for proper eager loading
    /// and null handling.
    /// </summary>
    public override async Task<ProjectManager?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(pm => pm.Projects).FirstOrDefaultAsync(pm => pm.Id == id);
    }
}
