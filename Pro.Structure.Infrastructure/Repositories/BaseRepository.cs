using Microsoft.EntityFrameworkCore;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Infrastructure.Data;

namespace Pro.Structure.Infrastructure.Repositories;

/// <summary>
/// Generic base repository implementing common CRUD operations.
/// Provides basic database operations for all entity types.
/// </summary>
/// <typeparam name="T">The entity type that inherits from BaseEntity</typeparam>
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

    /// <summary>
    /// Retrieves all entities of type T from the database.
    /// </summary>
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    /// <summary>
    /// Retrieves a single entity by its ID.
    /// Returns null if not found.
    /// </summary>
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Adds a new entity to the database.
    /// Returns true if successful, false if failed.
    /// </summary>
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

    /// <summary>
    /// Updates an existing entity in the database.
    /// Automatically updates the Modified timestamp.
    /// Returns true if successful, false if failed.
    /// </summary>
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

    /// <summary>
    /// Deletes an entity from the database by its ID.
    /// Returns true if successful, false if failed or not found.
    /// </summary>
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

    /// <summary>
    /// Checks if an entity with the given ID exists.
    /// </summary>
    public virtual async Task<bool> ExistsAsync(int id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id);
    }
}
