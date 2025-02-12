using Microsoft.AspNetCore.Mvc;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;
using Pro.Structure.Web.ViewModels;

namespace Pro.Structure.Web.Controllers;

public class StatusController : BaseController
{
    private readonly IStatusService _statusService;
    private readonly IProjectService _projectService;

    public StatusController(IStatusService statusService, IProjectService projectService)
    {
        _statusService = statusService;
        _projectService = projectService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var result = await _statusService.GetAllAsync();
            if (!result.Success)
                return HandleError(new Exception(result.Message));

            var viewModels = result.Data.Select(s => MapToViewModel(s)).ToList();

            // Fetch project counts for each status
            foreach (var viewModel in viewModels)
            {
                var projectsResult = await _projectService.GetProjectsByStatusAsync(viewModel.Id);
                if (projectsResult.Success)
                {
                    viewModel.ProjectCount = projectsResult.Data.Count();
                }
            }

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
            var result = await _statusService.GetByIdAsync(id);
            if (!result.Success)
                return HandleNotFound(result.Message);

            var viewModel = MapToViewModel(result.Data);
            var projectsResult = await _projectService.GetProjectsByStatusAsync(id);
            if (projectsResult.Success)
            {
                viewModel.ProjectCount = projectsResult.Data.Count();
            }
            return View(viewModel);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    public IActionResult Create()
    {
        return View(new StatusViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StatusViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var status = MapToEntity(viewModel);
            status.Created = DateTime.Now;
            status.Modified = DateTime.Now;

            var result = await _statusService.AddAsync(status);
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
            var result = await _statusService.GetByIdAsync(id);
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, StatusViewModel viewModel)
    {
        try
        {
            if (id != viewModel.Id)
                return HandleNotFound($"Status with ID {id} was not found.");

            if (!ModelState.IsValid)
                return View(viewModel);

            var status = MapToEntity(viewModel);
            var result = await _statusService.UpdateAsync(status);
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
            var result = await _statusService.GetByIdAsync(id);
            if (!result.Success)
                return HandleNotFound(result.Message);

            var viewModel = MapToViewModel(result.Data);
            var projectsResult = await _projectService.GetProjectsByStatusAsync(id);
            if (projectsResult.Success)
            {
                viewModel.ProjectCount = projectsResult.Data.Count();
            }
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
            var result = await _statusService.DeleteAsync(id);
            if (!result.Success)
                return HandleError(new Exception(result.Message));

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    private static StatusViewModel MapToViewModel(StatusModel status)
    {
        return new StatusViewModel
        {
            Id = status.Id,
            Name = status.Name,
            Description = status.Description,
            Created = status.Created,
            Modified = status.Modified,
        };
    }

    private static Status MapToEntity(StatusViewModel viewModel)
    {
        return new Status
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Description = viewModel.Description,
            Created = viewModel.Created,
            Modified = viewModel.Modified,
        };
    }
}
