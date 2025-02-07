using Microsoft.EntityFrameworkCore;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Infrastructure.Data;

namespace Pro.Structure.Infrastructure.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context)
        : base(context) { }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await _dbSet.Include(c => c.Projects).FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _dbSet.AnyAsync(c => c.Email == email);
    }

    public override async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _dbSet.Include(c => c.Projects).ToListAsync();
    }

    public override async Task<Customer?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(c => c.Projects).FirstOrDefaultAsync(c => c.Id == id);
    }
}
