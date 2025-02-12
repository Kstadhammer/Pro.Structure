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
                return View(viewModel);
            }

            var project = MapToEntity(viewModel);
            var result = await _projectService.UpdateAsync(project);
            if (!result.Success)
                return HandleError(new Exception(result.Message));

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Edit POST action for project {ProjectId}", id);
            await PopulateDropDownLists(); // Repopulate lists if there's an error
            return HandleError(ex);
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
            // Get statuses first since they're required
            var statusesResult = await _statusService.GetAllAsync();
            var statusList =
                statusesResult.Success && statusesResult.Data != null
                    ? statusesResult.Data.ToList()
                    : new List<StatusModel>();

            ViewBag.Statuses = statusList;

            // Get customers
            var customersResult = await _customerService.GetAllAsync();
            var customerList =
                customersResult.Success && customersResult.Data != null
                    ? customersResult.Data.ToList()
                    : new List<CustomerModel>();

            ViewBag.Customers = customerList;

            // Get project managers
            var managersResult = await _projectManagerService.GetAllAsync();
            var managerList =
                managersResult.Success && managersResult.Data != null
                    ? managersResult.Data.ToList()
                    : new List<ProjectManagerModel>();

            ViewBag.ProjectManagers = managerList;

            if (!statusList.Any())
            {
                _logger.LogWarning("No statuses available in the database");
            }
            if (!customerList.Any())
            {
                _logger.LogWarning("No customers available in the database");
            }
            if (!managerList.Any())
            {
                _logger.LogWarning("No project managers available in the database");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error populating dropdown lists");
            ViewBag.Statuses = new List<StatusModel>();
            ViewBag.Customers = new List<CustomerModel>();
            ViewBag.ProjectManagers = new List<ProjectManagerModel>();
            throw;
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
            Name = viewModel.Name,
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
