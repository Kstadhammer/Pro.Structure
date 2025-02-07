using Microsoft.EntityFrameworkCore;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Infrastructure.Data;

namespace Pro.Structure.Infrastructure.Repositories;

public class StatusRepository : BaseRepository<Status>, IStatusRepository
{
    public StatusRepository(ApplicationDbContext context)
        : base(context) { }

    public async Task<Status?> GetByNameAsync(string name)
    {
        return await _dbSet.Include(s => s.Projects).FirstOrDefaultAsync(s => s.Name == name);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbSet.AnyAsync(s => s.Name == name);
    }

    public override async Task<IEnumerable<Status>> GetAllAsync()
    {
        return await _dbSet.Include(s => s.Projects).ToListAsync();
    }

    public override async Task<Status?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(s => s.Projects).FirstOrDefaultAsync(s => s.Id == id);
    }
}
