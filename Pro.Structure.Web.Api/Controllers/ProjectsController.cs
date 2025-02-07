using Microsoft.AspNetCore.Mvc;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Pro.Structure.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all projects",
        Description = "Retrieves a list of all projects"
    )]
    [ProducesResponseType(
        typeof(ServiceResponse<IEnumerable<ProjectModel>>),
        StatusCodes.Status200OK
    )]
    public async Task<IActionResult> GetAll()
    {
        var result = await _projectService.GetAllAsync();
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get project by ID",
        Description = "Retrieves a specific project by its ID"
    )]
    [ProducesResponseType(typeof(ServiceResponse<ProjectModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _projectService.GetByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("customer/{customerId:int}")]
    [SwaggerOperation(
        Summary = "Get projects by customer",
        Description = "Retrieves all projects for a specific customer"
    )]
    [ProducesResponseType(
        typeof(ServiceResponse<IEnumerable<ProjectModel>>),
        StatusCodes.Status200OK
    )]
    public async Task<IActionResult> GetByCustomer(int customerId)
    {
        var result = await _projectService.GetProjectsByCustomerAsync(customerId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("status/{statusId:int}")]
    [SwaggerOperation(
        Summary = "Get projects by status",
        Description = "Retrieves all projects with a specific status"
    )]
    [ProducesResponseType(
        typeof(ServiceResponse<IEnumerable<ProjectModel>>),
        StatusCodes.Status200OK
    )]
    public async Task<IActionResult> GetByStatus(int statusId)
    {
        var result = await _projectService.GetProjectsByStatusAsync(statusId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("manager/{managerId:int}")]
    [SwaggerOperation(
        Summary = "Get projects by manager",
        Description = "Retrieves all projects assigned to a specific manager"
    )]
    [ProducesResponseType(
        typeof(ServiceResponse<IEnumerable<ProjectModel>>),
        StatusCodes.Status200OK
    )]
    public async Task<IActionResult> GetByManager(int managerId)
    {
        var result = await _projectService.GetProjectsByManagerAsync(managerId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create project", Description = "Creates a new project")]
    [ProducesResponseType(typeof(ServiceResponse<Project>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(Project project)
    {
        var projectNumber = await _projectService.GenerateProjectNumberAsync();
        if (!projectNumber.Success)
            return BadRequest(projectNumber);

        project.ProjectNumber = projectNumber.Data;
        var result = await _projectService.AddAsync(project);

        return result.Success
            ? CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result)
            : BadRequest(result);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Update project", Description = "Updates an existing project")]
    [ProducesResponseType(typeof(ServiceResponse<Project>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, Project project)
    {
        if (id != project.Id)
            return BadRequest("ID mismatch");

        var result = await _projectService.UpdateAsync(project);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPut("{id:int}/status/{statusId:int}")]
    [SwaggerOperation(
        Summary = "Update project status",
        Description = "Updates the status of an existing project"
    )]
    [ProducesResponseType(typeof(ServiceResponse<ProjectModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(int id, int statusId)
    {
        var result = await _projectService.UpdateProjectStatusAsync(id, statusId);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Delete project", Description = "Deletes a specific project")]
    [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _projectService.DeleteAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
