using Microsoft.AspNetCore.Mvc;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Pro.Structure.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectManagersController : ControllerBase
{
    private readonly IProjectManagerService _projectManagerService;

    public ProjectManagersController(IProjectManagerService projectManagerService)
    {
        _projectManagerService = projectManagerService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all project managers",
        Description = "Retrieves a list of all project managers"
    )]
    [ProducesResponseType(
        typeof(ServiceResponse<IEnumerable<ProjectManagerModel>>),
        StatusCodes.Status200OK
    )]
    public async Task<IActionResult> GetAll()
    {
        var result = await _projectManagerService.GetAllAsync();
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get project manager by ID",
        Description = "Retrieves a specific project manager by their ID"
    )]
    [ProducesResponseType(typeof(ServiceResponse<ProjectManagerModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _projectManagerService.GetByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("email/{email}")]
    [SwaggerOperation(
        Summary = "Get project manager by email",
        Description = "Retrieves a specific project manager by their email address"
    )]
    [ProducesResponseType(typeof(ServiceResponse<ProjectManagerModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await _projectManagerService.GetByEmailAsync(email);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("available")]
    [SwaggerOperation(
        Summary = "Get available project managers",
        Description = "Retrieves a list of project managers who can take on new projects"
    )]
    [ProducesResponseType(
        typeof(ServiceResponse<IEnumerable<ProjectManagerModel>>),
        StatusCodes.Status200OK
    )]
    public async Task<IActionResult> GetAvailableManagers()
    {
        var result = await _projectManagerService.GetAvailableManagersAsync();
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id:int}/projects")]
    [SwaggerOperation(
        Summary = "Get manager projects",
        Description = "Retrieves all projects assigned to a specific project manager"
    )]
    [ProducesResponseType(
        typeof(ServiceResponse<IEnumerable<ProjectModel>>),
        StatusCodes.Status200OK
    )]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetManagerProjects(int id)
    {
        var result = await _projectManagerService.GetManagerProjectsAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create project manager",
        Description = "Creates a new project manager"
    )]
    [ProducesResponseType(typeof(ServiceResponse<ProjectManager>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(ProjectManager projectManager)
    {
        var exists = await _projectManagerService.ExistsByEmailAsync(projectManager.Email);
        if (exists.Data)
            return BadRequest(
                ServiceResponse<ProjectManager>.Fail(
                    "Project manager with this email already exists"
                )
            );

        var result = await _projectManagerService.AddAsync(projectManager);
        return result.Success
            ? CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result)
            : BadRequest(result);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update project manager",
        Description = "Updates an existing project manager"
    )]
    [ProducesResponseType(typeof(ServiceResponse<ProjectManager>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, ProjectManager projectManager)
    {
        if (id != projectManager.Id)
            return BadRequest("ID mismatch");

        var result = await _projectManagerService.UpdateAsync(projectManager);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Delete project manager",
        Description = "Deletes a specific project manager"
    )]
    [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _projectManagerService.DeleteAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
