using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Factories;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Infrastructure.Services;

public class ProjectManagerService : BaseService<ProjectManager>, IProjectManagerService
{
    private readonly IProjectManagerRepository _projectManagerRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IFactory<ProjectManager, ProjectManagerModel> _managerFactory;
    private readonly IFactory<Project, ProjectModel> _projectFactory;

    public ProjectManagerService(
        IProjectManagerRepository projectManagerRepository,
        IProjectRepository projectRepository,
        IFactory<ProjectManager, ProjectManagerModel> managerFactory,
        IFactory<Project, ProjectModel> projectFactory
    )
        : base(projectManagerRepository)
    {
        _projectManagerRepository = projectManagerRepository;
        _projectRepository = projectRepository;
        _managerFactory = managerFactory;
        _projectFactory = projectFactory;
    }

    public async Task<ServiceResponse<ProjectManagerModel>> GetByEmailAsync(string email)
    {
        try
        {
            var manager = await _projectManagerRepository.GetByEmailAsync(email);
            if (manager == null)
                return ServiceResponse<ProjectManagerModel>.Fail("Project manager not found");

            return ServiceResponse<ProjectManagerModel>.Ok(_managerFactory.CreateModel(manager));
        }
        catch (Exception ex)
        {
            return ServiceResponse<ProjectManagerModel>.Fail(
                $"Error retrieving project manager: {ex.Message}"
            );
        }
    }

    public async Task<ServiceResponse<bool>> ExistsByEmailAsync(string email)
    {
        try
        {
            var exists = await _projectManagerRepository.ExistsByEmailAsync(email);
            return ServiceResponse<bool>.Ok(exists);
        }
        catch (Exception ex)
        {
            return ServiceResponse<bool>.Fail(
                $"Error checking project manager existence: {ex.Message}"
            );
        }
    }

    public async Task<ServiceResponse<IEnumerable<ProjectManagerModel>>> GetAvailableManagersAsync()
    {
        try
        {
            var managers = await _projectManagerRepository.GetAvailableManagersAsync();
            var models = managers.Select(m => _managerFactory.CreateModel(m));
            return ServiceResponse<IEnumerable<ProjectManagerModel>>.Ok(models);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<ProjectManagerModel>>.Fail(
                $"Error retrieving available managers: {ex.Message}"
            );
        }
    }

    public async Task<ServiceResponse<IEnumerable<ProjectModel>>> GetManagerProjectsAsync(
        int managerId
    )
    {
        try
        {
            var projects = await _projectRepository.GetProjectsByManagerAsync(managerId);
            var models = projects.Select(p => _projectFactory.CreateModel(p));
            return ServiceResponse<IEnumerable<ProjectModel>>.Ok(models);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<ProjectModel>>.Fail(
                $"Error retrieving manager projects: {ex.Message}"
            );
        }
    }

    public new async Task<ServiceResponse<IEnumerable<ProjectManagerModel>>> GetAllAsync()
    {
        var result = await base.GetAllAsync();
        if (!result.Success)
            return ServiceResponse<IEnumerable<ProjectManagerModel>>.Fail(result.Message);

        try
        {
            var models = result.Data.Select(m => _managerFactory.CreateModel(m));
            return ServiceResponse<IEnumerable<ProjectManagerModel>>.Ok(models);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<ProjectManagerModel>>.Fail(
                $"Error mapping project managers: {ex.Message}"
            );
        }
    }

    public new async Task<ServiceResponse<ProjectManagerModel>> GetByIdAsync(int id)
    {
        var result = await base.GetByIdAsync(id);
        if (!result.Success)
            return ServiceResponse<ProjectManagerModel>.Fail(result.Message);

        try
        {
            var model = _managerFactory.CreateModel(result.Data);
            return ServiceResponse<ProjectManagerModel>.Ok(model);
        }
        catch (Exception ex)
        {
            return ServiceResponse<ProjectManagerModel>.Fail(
                $"Error mapping project manager: {ex.Message}"
            );
        }
    }

    public override async Task<ServiceResponse<bool>> DeleteAsync(int id)
    {
        try
        {
            var managerProjects = await _projectRepository.GetProjectsByManagerAsync(id);
            if (managerProjects.Any())
            {
                return ServiceResponse<bool>.Fail(
                    "Cannot delete project manager with active projects"
                );
            }

            return await base.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            return ServiceResponse<bool>.Fail($"Error deleting project manager: {ex.Message}");
        }
    }
}
