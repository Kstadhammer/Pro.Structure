using Microsoft.EntityFrameworkCore;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Infrastructure.Data;

namespace Pro.Structure.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Customer entity.
/// 
/// This implementation was developed with AI assistance for:
/// - Efficient eager loading of related entities
/// - Email-based customer lookup and validation
/// - Optimized query patterns
/// - Entity relationship management
/// </summary>
public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context)
        : base(context) { }

    /// <summary>
    /// Retrieves a customer by their email address.
    /// Implementation assisted by AI for proper eager loading
    /// of related projects.
    /// </summary>
    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await _dbSet.Include(c => c.Projects).FirstOrDefaultAsync(c => c.Email == email);
    }

    /// <summary>
    /// Checks if a customer with the given email exists.
    /// Implementation assisted by AI for efficient existence checking.
    /// </summary>
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _dbSet.AnyAsync(c => c.Email == email);
    }

    /// <summary>
    /// Retrieves all customers with their related projects.
    /// Implementation assisted by AI for proper eager loading
    /// and query optimization.
    /// </summary>
    public override async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _dbSet.Include(c => c.Projects).ToListAsync();
    }

    /// <summary>
    /// Retrieves a customer by ID with their related projects.
    /// Implementation assisted by AI for proper eager loading
    /// and null handling.
    /// </summary>
    public override async Task<Customer?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(c => c.Projects).FirstOrDefaultAsync(c => c.Id == id);
    }
}
