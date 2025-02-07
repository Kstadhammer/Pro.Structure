using Microsoft.AspNetCore.Mvc;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;
using Pro.Structure.Web.ViewModels;

namespace Pro.Structure.Web.Controllers;

public class CustomersController : BaseController
{
    private readonly ICustomerService _customerService;
    private readonly IProjectService _projectService;

    public CustomersController(ICustomerService customerService, IProjectService projectService)
    {
        _customerService = customerService;
        _projectService = projectService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var result = await _customerService.GetAllAsync();
            if (!result.Success)
                return HandleError(new Exception(result.Message));

            var viewModels = result.Data.Select(c => MapToViewModel(c)).ToList();
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
            var result = await _customerService.GetByIdAsync(id);
            if (!result.Success)
                return HandleNotFound(result.Message);

            var viewModel = MapToViewModel(result.Data);
            var projectsResult = await _projectService.GetProjectsByCustomerAsync(id);
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
        return View(new CustomerViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CustomerViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var customer = MapToEntity(viewModel);
            var result = await _customerService.AddAsync(customer);
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
            var result = await _customerService.GetByIdAsync(id);
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
    public async Task<IActionResult> Edit(int id, CustomerViewModel viewModel)
    {
        try
        {
            if (id != viewModel.Id)
                return HandleNotFound($"Customer with ID {id} was not found.");

            if (!ModelState.IsValid)
                return View(viewModel);

            var customer = MapToEntity(viewModel);
            var result = await _customerService.UpdateAsync(customer);
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
            var result = await _customerService.GetByIdAsync(id);
            if (!result.Success)
                return HandleNotFound(result.Message);

            var viewModel = MapToViewModel(result.Data);
            var projectsResult = await _projectService.GetProjectsByCustomerAsync(id);
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
            var result = await _customerService.DeleteAsync(id);
            if (!result.Success)
                return HandleError(new Exception(result.Message));

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    private static CustomerViewModel MapToViewModel(CustomerModel customer)
    {
        return new CustomerViewModel
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Created = customer.Created,
            Modified = customer.Modified,
        };
    }

    private static Customer MapToEntity(CustomerViewModel viewModel)
    {
        return new Customer
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Email = viewModel.Email,
            PhoneNumber = viewModel.PhoneNumber,
            Created = viewModel.Created,
            Modified = viewModel.Modified,
        };
    }
}
