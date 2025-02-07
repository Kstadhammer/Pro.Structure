using Microsoft.EntityFrameworkCore;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Infrastructure.Data;

namespace Pro.Structure.Infrastructure.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext context)
        : base(context) { }

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

    public async Task<IEnumerable<Project>> GetProjectsByCustomerAsync(int customerId)
    {
        return await _dbSet
            .Include(p => p.Customer)
            .Include(p => p.Status)
            .Include(p => p.ProjectManager)
            .Where(p => p.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Project>> GetProjectsByStatusAsync(int statusId)
    {
        return await _dbSet
            .Include(p => p.Customer)
            .Include(p => p.Status)
            .Include(p => p.ProjectManager)
            .Where(p => p.StatusId == statusId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Project>> GetProjectsByManagerAsync(int projectManagerId)
    {
        return await _dbSet
            .Include(p => p.Customer)
            .Include(p => p.Status)
            .Include(p => p.ProjectManager)
            .Where(p => p.ProjectManagerId == projectManagerId)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _dbSet
            .Include(p => p.Customer)
            .Include(p => p.Status)
            .Include(p => p.ProjectManager)
            .ToListAsync();
    }

    public override async Task<Project?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Customer)
            .Include(p => p.Status)
            .Include(p => p.ProjectManager)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
