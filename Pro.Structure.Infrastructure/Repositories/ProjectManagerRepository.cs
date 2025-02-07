using Microsoft.EntityFrameworkCore;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Infrastructure.Data;

namespace Pro.Structure.Infrastructure.Repositories;

public class ProjectManagerRepository : BaseRepository<ProjectManager>, IProjectManagerRepository
{
    public ProjectManagerRepository(ApplicationDbContext context)
        : base(context) { }

    public async Task<ProjectManager?> GetByEmailAsync(string email)
    {
        return await _dbSet.Include(pm => pm.Projects).FirstOrDefaultAsync(pm => pm.Email == email);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _dbSet.AnyAsync(pm => pm.Email == email);
    }

    public async Task<IEnumerable<ProjectManager>> GetAvailableManagersAsync()
    {
        return await _dbSet
            .Include(pm => pm.Projects)
            .Where(pm => pm.Projects.Count < 5) // Assuming a manager can handle up to 5 projects
            .ToListAsync();
    }

    public override async Task<IEnumerable<ProjectManager>> GetAllAsync()
    {
        return await _dbSet.Include(pm => pm.Projects).ToListAsync();
    }

    public override async Task<ProjectManager?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(pm => pm.Projects).FirstOrDefaultAsync(pm => pm.Id == id);
    }
}
