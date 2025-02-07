using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Factories;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;

namespace Pro.Structure.Infrastructure.Services;

public class ProjectService : BaseService<Project>, IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IFactory<Project, ProjectModel> _projectFactory;

    public ProjectService(
        IProjectRepository projectRepository,
        IFactory<Project, ProjectModel> projectFactory
    )
        : base(projectRepository)
    {
        _projectRepository = projectRepository;
        _projectFactory = projectFactory;
    }

    public async Task<ServiceResponse<string>> GenerateProjectNumberAsync()
    {
        try
        {
            var projectNumber = await _projectRepository.GenerateProjectNumberAsync();
            return ServiceResponse<string>.Ok(projectNumber);
        }
        catch (Exception ex)
        {
            return ServiceResponse<string>.Fail($"Error generating project number: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<IEnumerable<ProjectModel>>> GetProjectsByCustomerAsync(
        int customerId
    )
    {
        try
        {
            var projects = await _projectRepository.GetProjectsByCustomerAsync(customerId);
            var models = projects.Select(p => _projectFactory.CreateModel(p));
            return ServiceResponse<IEnumerable<ProjectModel>>.Ok(models);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<ProjectModel>>.Fail(
                $"Error retrieving customer projects: {ex.Message}"
            );
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

    public async Task<ServiceResponse<IEnumerable<ProjectModel>>> GetProjectsByManagerAsync(
        int projectManagerId
    )
    {
        try
        {
            var projects = await _projectRepository.GetProjectsByManagerAsync(projectManagerId);
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

    public async Task<ServiceResponse<ProjectModel>> UpdateProjectStatusAsync(
        int projectId,
        int statusId
    )
    {
        try
        {
            var projectResult = await base.GetByIdAsync(projectId);
            if (!projectResult.Success)
                return ServiceResponse<ProjectModel>.Fail(projectResult.Message);

            var project = projectResult.Data;
            project.StatusId = statusId;
            project.Modified = DateTime.Now;

            var result = await base.UpdateAsync(project);
            if (!result.Success)
                return ServiceResponse<ProjectModel>.Fail(result.Message);

            return ServiceResponse<ProjectModel>.Ok(_projectFactory.CreateModel(result.Data));
        }
        catch (Exception ex)
        {
            return ServiceResponse<ProjectModel>.Fail(
                $"Error updating project status: {ex.Message}"
            );
        }
    }

    public new async Task<ServiceResponse<IEnumerable<ProjectModel>>> GetAllAsync()
    {
        var result = await base.GetAllAsync();
        if (!result.Success)
            return ServiceResponse<IEnumerable<ProjectModel>>.Fail(result.Message);

        try
        {
            var models = result.Data.Select(p => _projectFactory.CreateModel(p));
            return ServiceResponse<IEnumerable<ProjectModel>>.Ok(models);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<ProjectModel>>.Fail(
                $"Error mapping projects: {ex.Message}"
            );
        }
    }

    public new async Task<ServiceResponse<ProjectModel>> GetByIdAsync(int id)
    {
        var result = await base.GetByIdAsync(id);
        if (!result.Success)
            return ServiceResponse<ProjectModel>.Fail(result.Message);

        try
        {
            var model = _projectFactory.CreateModel(result.Data);
            return ServiceResponse<ProjectModel>.Ok(model);
        }
        catch (Exception ex)
        {
            return ServiceResponse<ProjectModel>.Fail($"Error mapping project: {ex.Message}");
        }
    }
}
