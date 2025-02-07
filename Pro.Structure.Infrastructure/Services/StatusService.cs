using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Factories;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Infrastructure.Services;

public class StatusService : BaseService<Status>, IStatusService
{
    private readonly IStatusRepository _statusRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IFactory<Status, StatusModel> _statusFactory;
    private readonly IFactory<Project, ProjectModel> _projectFactory;

    public StatusService(
        IStatusRepository statusRepository,
        IProjectRepository projectRepository,
        IFactory<Status, StatusModel> statusFactory,
        IFactory<Project, ProjectModel> projectFactory
    )
        : base(statusRepository)
    {
        _statusRepository = statusRepository;
        _projectRepository = projectRepository;
        _statusFactory = statusFactory;
        _projectFactory = projectFactory;
    }

    public async Task<ServiceResponse<StatusModel>> GetByNameAsync(string name)
    {
        try
        {
            var status = await _statusRepository.GetByNameAsync(name);
            if (status == null)
                return ServiceResponse<StatusModel>.Fail("Status not found");

            return ServiceResponse<StatusModel>.Ok(_statusFactory.CreateModel(status));
        }
        catch (Exception ex)
        {
            return ServiceResponse<StatusModel>.Fail($"Error retrieving status: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<bool>> ExistsByNameAsync(string name)
    {
        try
        {
            var exists = await _statusRepository.ExistsByNameAsync(name);
            return ServiceResponse<bool>.Ok(exists);
        }
        catch (Exception ex)
        {
            return ServiceResponse<bool>.Fail($"Error checking status existence: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<IEnumerable<ProjectModel>>> GetProjectsByStatusAsync(
        int statusId
    )
    {
        try
        {
            var projects = await _projectRepository.GetProjectsByStatusAsync(statusId);
            var models = projects.Select(p => _projectFactory.CreateModel(p));
            return ServiceResponse<IEnumerable<ProjectModel>>.Ok(models);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<ProjectModel>>.Fail(
                $"Error retrieving projects by status: {ex.Message}"
            );
        }
    }

    public new async Task<ServiceResponse<IEnumerable<StatusModel>>> GetAllAsync()
    {
        var result = await base.GetAllAsync();
        if (!result.Success)
            return ServiceResponse<IEnumerable<StatusModel>>.Fail(result.Message);

        try
        {
            var models = result.Data.Select(s => _statusFactory.CreateModel(s));
            return ServiceResponse<IEnumerable<StatusModel>>.Ok(models);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<StatusModel>>.Fail(
                $"Error mapping statuses: {ex.Message}"
            );
        }
    }

    public new async Task<ServiceResponse<StatusModel>> GetByIdAsync(int id)
    {
        var result = await base.GetByIdAsync(id);
        if (!result.Success)
            return ServiceResponse<StatusModel>.Fail(result.Message);

        try
        {
            var model = _statusFactory.CreateModel(result.Data);
            return ServiceResponse<StatusModel>.Ok(model);
        }
        catch (Exception ex)
        {
            return ServiceResponse<StatusModel>.Fail($"Error mapping status: {ex.Message}");
        }
    }

    public override async Task<ServiceResponse<bool>> DeleteAsync(int id)
    {
        try
        {
            var statusProjects = await _projectRepository.GetProjectsByStatusAsync(id);
            if (statusProjects.Any())
            {
                return ServiceResponse<bool>.Fail("Cannot delete status with associated projects");
            }

            return await base.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            return ServiceResponse<bool>.Fail($"Error deleting status: {ex.Message}");
        }
    }
}
