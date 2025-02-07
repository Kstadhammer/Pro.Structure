using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;
using Pro.Structure.Web.ViewModels;

namespace Pro.Structure.Web.Controllers;

public class ProjectsController : BaseController
{
    private readonly IProjectService _projectService;
    private readonly ICustomerService _customerService;
    private readonly IProjectManagerService _projectManagerService;
    private readonly IStatusService _statusService;

    public ProjectsController(
        IProjectService projectService,
        ICustomerService customerService,
        IProjectManagerService projectManagerService,
        IStatusService statusService
    )
    {
        _projectService = projectService;
        _customerService = customerService;
        _projectManagerService = projectManagerService;
        _statusService = statusService;
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
                await PopulateDropDownLists();
                return View(viewModel);
            }

            var project = MapToEntity(viewModel);
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
            var result = await _projectService.GetByIdAsync(id);
            if (!result.Success)
                return HandleNotFound(result.Message);

            await PopulateDropDownLists();
            var viewModel = MapToViewModel(result.Data);
            return View(viewModel);
        }
        catch (Exception ex)
        {
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
        var customersResult = await _customerService.GetAllAsync();
        ViewBag.Customers = new SelectList(
            customersResult.Data ?? Enumerable.Empty<CustomerModel>(),
            "Id",
            "Name"
        );

        var managersResult = await _projectManagerService.GetAllAsync();
        ViewBag.ProjectManagers = new SelectList(
            managersResult.Data ?? Enumerable.Empty<ProjectManagerModel>(),
            "Id",
            "FullName"
        );

        var statusesResult = await _statusService.GetAllAsync();
        ViewBag.Statuses = new SelectList(
            statusesResult.Data ?? Enumerable.Empty<StatusModel>(),
            "Id",
            "Name"
        );
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
            ProjectNumber = viewModel.ProjectNumber,
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
