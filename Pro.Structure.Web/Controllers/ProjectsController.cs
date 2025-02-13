using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;
using Pro.Structure.Web.ViewModels;

namespace Pro.Structure.Web.Controllers;

[Authorize]
public class ProjectsController : BaseController
{
    private readonly IProjectService _projectService;
    private readonly ICustomerService _customerService;
    private readonly IProjectManagerService _projectManagerService;
    private readonly IStatusService _statusService;
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(
        IProjectService projectService,
        ICustomerService customerService,
        IProjectManagerService projectManagerService,
        IStatusService statusService,
        ILogger<ProjectsController> logger
    )
    {
        _projectService = projectService;
        _customerService = customerService;
        _projectManagerService = projectManagerService;
        _statusService = statusService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var result = await _projectService.GetAllAsync();
            if (!result.Success)
                return HandleError(new Exception(result.Message));

            var viewModels = result.Data.Select(p => MapToViewModel(p)).ToList();
            return View(viewModels);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var result = await _projectService.GetByIdAsync(id);
            if (!result.Success)
                return HandleNotFound(result.Message);

            var viewModel = MapToViewModel(result.Data);
            return View(viewModel);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    public async Task<IActionResult> Create()
    {
        try
        {
            await PopulateDropDownLists();
            return View(new ProjectViewModel());
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProjectViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                // Log validation errors
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        _logger.LogError("Validation error: {ErrorMessage}", error.ErrorMessage);
                    }
                }

                await PopulateDropDownLists();
                return View(viewModel);
            }

            // Generate project number
            var projectNumberResult = await _projectService.GenerateProjectNumberAsync();
            if (!projectNumberResult.Success)
                return HandleError(new Exception(projectNumberResult.Message));

            var project = MapToEntity(viewModel);
            project.ProjectNumber = projectNumberResult.Data;

            var result = await _projectService.AddAsync(project);
            if (!result.Success)
                return HandleError(new Exception(result.Message));

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            // First get the project
            var result = await _projectService.GetByIdAsync(id);
            if (!result.Success)
                return HandleNotFound(result.Message);

            // Then populate all dropdown lists
            await PopulateDropDownLists();

            // Finally, map to view model and return the view
            var viewModel = MapToViewModel(result.Data);

            // Double check that ViewBag values are set
            if (ViewBag.Statuses == null)
            {
                ViewBag.Statuses = new SelectList(new List<StatusModel>(), "Id", "Name");
            }
            if (ViewBag.Customers == null)
            {
                ViewBag.Customers = new SelectList(new List<CustomerModel>(), "Id", "Name");
            }
            if (ViewBag.ProjectManagers == null)
            {
                ViewBag.ProjectManagers = new SelectList(
                    new List<ProjectManagerModel>(),
                    "Id",
                    "FullName"
                );
            }

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Edit action for project {ProjectId}", id);
            return HandleError(ex);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProjectViewModel viewModel)
    {
        try
        {
            if (id != viewModel.Id)
                return HandleNotFound($"Project with ID {id} was not found.");

            if (!ModelState.IsValid)
            {
                // Repopulate dropdown lists if validation fails
                await PopulateDropDownLists();

                // Double check that ViewBag values are set
                if (ViewBag.Statuses == null)
                {
                    ViewBag.Statuses = new SelectList(new List<StatusModel>(), "Id", "Name");
                }
                if (ViewBag.Customers == null)
                {
                    ViewBag.Customers = new SelectList(new List<CustomerModel>(), "Id", "Name");
                }
                if (ViewBag.ProjectManagers == null)
                {
                    ViewBag.ProjectManagers = new SelectList(
                        new List<ProjectManagerModel>(),
                        "Id",
                        "FullName"
                    );
                }

                return View(viewModel);
            }

            // Get the existing project to preserve created date and ensure it exists
            var existingProject = await _projectService.GetByIdAsync(id);
            if (!existingProject.Success)
                return HandleNotFound(existingProject.Message);

            var project = MapToEntity(viewModel);
            project.Created = existingProject.Data.Created;
            project.Modified = DateTime.Now;

            var result = await _projectService.UpdateAsync(project);
            if (!result.Success)
            {
                _logger.LogError("Failed to update project: {Message}", result.Message);
                ModelState.AddModelError(string.Empty, result.Message);
                await PopulateDropDownLists();
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Edit POST action for project {ProjectId}", id);
            ModelState.AddModelError(string.Empty, $"Error updating project: {ex.Message}");
            await PopulateDropDownLists();
            return View(viewModel);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _projectService.GetByIdAsync(id);
            if (!result.Success)
                return HandleNotFound(result.Message);

            var viewModel = MapToViewModel(result.Data);
            return View(viewModel);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var result = await _projectService.DeleteAsync(id);
            if (!result.Success)
                return HandleError(new Exception(result.Message));

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    private async Task PopulateDropDownLists()
    {
        try
        {
            // Get statuses
            var statusesResult = await _statusService.GetAllAsync();
            var statusList =
                statusesResult.Success && statusesResult.Data != null
                    ? statusesResult.Data.ToList()
                    : new List<StatusModel>();
            ViewBag.Statuses = new SelectList(statusList, "Id", "Name");

            // Get customers
            var customersResult = await _customerService.GetAllAsync();
            var customerList =
                customersResult.Success && customersResult.Data != null
                    ? customersResult.Data.ToList()
                    : new List<CustomerModel>();
            ViewBag.Customers = new SelectList(customerList, "Id", "Name");

            // Get project managers
            var managersResult = await _projectManagerService.GetAllAsync();
            var managerList =
                managersResult.Success && managersResult.Data != null
                    ? managersResult.Data.ToList()
                    : new List<ProjectManagerModel>();
            ViewBag.ProjectManagers = new SelectList(managerList, "Id", "FullName");

            // Log warnings if any of the lists are empty
            if (!statusList.Any())
            {
                _logger.LogWarning(
                    "No statuses available: {Message}",
                    statusesResult.Success ? "Empty list" : statusesResult.Message
                );
            }
            if (!customerList.Any())
            {
                _logger.LogWarning(
                    "No customers available: {Message}",
                    customersResult.Success ? "Empty list" : customersResult.Message
                );
            }
            if (!managerList.Any())
            {
                _logger.LogWarning(
                    "No project managers available: {Message}",
                    managersResult.Success ? "Empty list" : managersResult.Message
                );
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error populating dropdown lists");

            // Ensure ViewBag properties are never null
            ViewBag.Statuses = new SelectList(new List<StatusModel>(), "Id", "Name");
            ViewBag.Customers = new SelectList(new List<CustomerModel>(), "Id", "Name");
            ViewBag.ProjectManagers = new SelectList(
                new List<ProjectManagerModel>(),
                "Id",
                "FullName"
            );
        }
    }

    private static ProjectViewModel MapToViewModel(ProjectModel project)
    {
        return new ProjectViewModel
        {
            Id = project.Id,
            ProjectNumber = project.ProjectNumber,
            Name = project.Name,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            HourlyRate = project.HourlyRate,
            TotalPrice = project.TotalPrice,
            ProjectManagerId = project.ProjectManagerId,
            ProjectManagerName = project.ProjectManagerName,
            CustomerId = project.CustomerId,
            CustomerName = project.CustomerName,
            StatusId = project.StatusId,
            StatusName = project.StatusName,
            Created = project.Created,
            Modified = project.Modified,
        };
    }

    private static Project MapToEntity(ProjectViewModel viewModel)
    {
        return new Project
        {
            Id = viewModel.Id,
            ProjectNumber = viewModel.ProjectNumber ?? string.Empty,
            Name = viewModel.Name ?? string.Empty,
            StartDate = viewModel.StartDate,
            EndDate = viewModel.EndDate,
            HourlyRate = viewModel.HourlyRate,
            TotalPrice = viewModel.TotalPrice,
            ProjectManagerId = viewModel.ProjectManagerId,
            CustomerId = viewModel.CustomerId,
            StatusId = viewModel.StatusId,
            Created = viewModel.Created,
            Modified = viewModel.Modified,
        };
    }
}
