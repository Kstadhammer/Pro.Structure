using Microsoft.EntityFrameworkCore;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Infrastructure.Data;

namespace Pro.Structure.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Status entity.
/// 
/// This implementation was developed with AI assistance for:
/// - Efficient eager loading of related projects
/// - Name-based status lookup and validation
/// - Query optimization patterns
/// - Entity relationship management
/// </summary>
public class StatusRepository : BaseRepository<Status>, IStatusRepository
{
    public StatusRepository(ApplicationDbContext context)
        : base(context) { }

    /// <summary>
    /// Retrieves a status by its name.
    /// Implementation assisted by AI for proper eager loading
    /// of related projects.
    /// </summary>
    public async Task<Status?> GetByNameAsync(string name)
    {
        return await _dbSet.Include(s => s.Projects).FirstOrDefaultAsync(s => s.Name == name);
    }

    /// <summary>
    /// Checks if a status with the given name exists.
    /// Implementation assisted by AI for efficient existence checking.
    /// </summary>
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbSet.AnyAsync(s => s.Name == name);
    }

    /// <summary>
    /// Retrieves all statuses with their related projects.
    /// Implementation assisted by AI for proper eager loading
    /// and query optimization.
    /// </summary>
    public override async Task<IEnumerable<Status>> GetAllAsync()
    {
        return await _dbSet.Include(s => s.Projects).ToListAsync();
    }

    /// <summary>
    /// Retrieves a status by ID with its related projects.
    /// Implementation assisted by AI for proper eager loading
    /// and null handling.
    /// </summary>
    public override async Task<Status?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(s => s.Projects).FirstOrDefaultAsync(s => s.Id == id);
    }
}
