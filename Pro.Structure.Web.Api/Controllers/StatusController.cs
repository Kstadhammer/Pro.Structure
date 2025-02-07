using Microsoft.AspNetCore.Mvc;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Pro.Structure.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusController : ControllerBase
{
    private readonly IStatusService _statusService;

    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all statuses",
        Description = "Retrieves a list of all project statuses"
    )]
    [ProducesResponseType(
        typeof(ServiceResponse<IEnumerable<StatusModel>>),
        StatusCodes.Status200OK
    )]
    public async Task<IActionResult> GetAll()
    {
        var result = await _statusService.GetAllAsync();
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get status by ID",
        Description = "Retrieves a specific status by its ID"
    )]
    [ProducesResponseType(typeof(ServiceResponse<StatusModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _statusService.GetByIdAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("name/{name}")]
    [SwaggerOperation(
        Summary = "Get status by name",
        Description = "Retrieves a specific status by its name"
    )]
    [ProducesResponseType(typeof(ServiceResponse<StatusModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByName(string name)
    {
        var result = await _statusService.GetByNameAsync(name);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("{id:int}/projects")]
    [SwaggerOperation(
        Summary = "Get projects by status",
        Description = "Retrieves all projects with a specific status"
    )]
    [ProducesResponseType(
        typeof(ServiceResponse<IEnumerable<ProjectModel>>),
        StatusCodes.Status200OK
    )]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStatusProjects(int id)
    {
        var result = await _statusService.GetProjectsByStatusAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create status", Description = "Creates a new project status")]
    [ProducesResponseType(typeof(ServiceResponse<Status>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(Status status)
    {
        var exists = await _statusService.ExistsByNameAsync(status.Name);
        if (exists.Data)
            return BadRequest(ServiceResponse<Status>.Fail("Status with this name already exists"));

        var result = await _statusService.AddAsync(status);
        return result.Success
            ? CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result)
            : BadRequest(result);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update status",
        Description = "Updates an existing project status"
    )]
    [ProducesResponseType(typeof(ServiceResponse<Status>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, Status status)
    {
        if (id != status.Id)
            return BadRequest("ID mismatch");

        var result = await _statusService.UpdateAsync(status);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Delete status", Description = "Deletes a specific project status")]
    [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _statusService.DeleteAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
