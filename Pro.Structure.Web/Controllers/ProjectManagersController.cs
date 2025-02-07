using Microsoft.AspNetCore.Mvc;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;
using Pro.Structure.Web.ViewModels;

namespace Pro.Structure.Web.Controllers;

public class ProjectManagersController : BaseController
{
    private readonly IProjectManagerService _projectManagerService;
    private readonly IProjectService _projectService;

    public ProjectManagersController(
        IProjectManagerService projectManagerService,
        IProjectService projectService
    )
    {
        _projectManagerService = projectManagerService;
        _projectService = projectService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var result = await _projectManagerService.GetAllAsync();
            if (!result.Success)
                return HandleError(new Exception(result.Message));

            var viewModels = result.Data.Select(m => MapToViewModel(m)).ToList();

            foreach (var viewModel in viewModels)
            {
                var projectsResult = await _projectService.GetProjectsByManagerAsync(viewModel.Id);
                if (projectsResult.Success)
                {
                    viewModel.TotalProjectCount = projectsResult.Data.Count();
                    viewModel.ActiveProjectCount = projectsResult.Data.Count(p =>
                        p.StatusName != "Completed"
                    );
                    viewModel.IsAvailable = viewModel.ActiveProjectCount < 5;
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
            var result = await _projectManagerService.GetByIdAsync(id);
            if (!result.Success)
                return HandleNotFound(result.Message);

            var viewModel = MapToViewModel(result.Data);
            var projectsResult = await _projectService.GetProjectsByManagerAsync(id);
            if (projectsResult.Success)
            {
                viewModel.TotalProjectCount = projectsResult.Data.Count();
                viewModel.ActiveProjectCount = projectsResult.Data.Count(p =>
                    p.StatusName != "Completed"
                );
                viewModel.IsAvailable = viewModel.ActiveProjectCount < 5;
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
        return View(new ProjectManagerViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProjectManagerViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var manager = MapToEntity(viewModel);
            var result = await _projectManagerService.AddAsync(manager);
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
            var result = await _projectManagerService.GetByIdAsync(id);
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
    public async Task<IActionResult> Edit(int id, ProjectManagerViewModel viewModel)
    {
        try
        {
            if (id != viewModel.Id)
                return HandleNotFound($"Project Manager with ID {id} was not found.");

            if (!ModelState.IsValid)
                return View(viewModel);

            var manager = MapToEntity(viewModel);
            var result = await _projectManagerService.UpdateAsync(manager);
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
            var result = await _projectManagerService.GetByIdAsync(id);
            if (!result.Success)
                return HandleNotFound(result.Message);

            var viewModel = MapToViewModel(result.Data);
            var projectsResult = await _projectService.GetProjectsByManagerAsync(id);
            if (projectsResult.Success)
            {
                viewModel.TotalProjectCount = projectsResult.Data.Count();
                viewModel.ActiveProjectCount = projectsResult.Data.Count(p =>
                    p.StatusName != "Completed"
                );
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
            var result = await _projectManagerService.DeleteAsync(id);
            if (!result.Success)
                return HandleError(new Exception(result.Message));

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    private static ProjectManagerViewModel MapToViewModel(ProjectManagerModel manager)
    {
        return new ProjectManagerViewModel
        {
            Id = manager.Id,
            Name = $"{manager.FirstName} {manager.LastName}",
            Email = manager.Email,
            PhoneNumber = manager.PhoneNumber,
            Created = manager.Created,
            Modified = manager.Modified,
        };
    }

    private static ProjectManager MapToEntity(ProjectManagerViewModel viewModel)
    {
        var nameParts = viewModel.Name.Split(' ', 2);
        return new ProjectManager
        {
            Id = viewModel.Id,
            FirstName = nameParts[0],
            LastName = nameParts.Length > 1 ? nameParts[1] : string.Empty,
            Email = viewModel.Email,
            PhoneNumber = viewModel.PhoneNumber,
            Created = viewModel.Created,
            Modified = viewModel.Modified,
        };
    }
}
