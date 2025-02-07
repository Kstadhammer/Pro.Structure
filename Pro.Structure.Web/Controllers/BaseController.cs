using Microsoft.AspNetCore.Mvc;
using Pro.Structure.Web.ViewModels;

namespace Pro.Structure.Web.Controllers;

public abstract class BaseController : Controller
{
    protected IActionResult HandleError(Exception ex)
    {
        // Log the error here if needed

        var errorViewModel = new ErrorViewModel
        {
            Message = "An error occurred while processing your request.",
            Details = ex.Message,
        };

        return View("Error", errorViewModel);
    }

    protected IActionResult HandleNotFound(string message = "The requested resource was not found.")
    {
        var errorViewModel = new ErrorViewModel { Message = message, StatusCode = 404 };

        return View("NotFound", errorViewModel);
    }
}
