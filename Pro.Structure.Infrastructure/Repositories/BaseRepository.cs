using Microsoft.EntityFrameworkCore;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Infrastructure.Data;

namespace Pro.Structure.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T>
    where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<bool> AddAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        catch
        {
            return false;
        }
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        try
        {
            entity.Modified = DateTime.Now;
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        catch
        {
            return false;
        }
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        catch
        {
            return false;
        }
    }

    public virtual async Task<bool> ExistsAsync(int id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id);
    }
}
