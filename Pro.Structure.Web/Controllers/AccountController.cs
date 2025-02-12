using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Infrastructure.Data;
using Pro.Structure.Web.ViewModels;

namespace Pro.Structure.Web.Controllers;

public class AccountController : BaseController
{
    private readonly IAuthService _authService;
    private readonly ILogger<AccountController> _logger;
    private readonly ApplicationDbContext _context;

    public AccountController(
        IAuthService authService,
        ILogger<AccountController> logger,
        ApplicationDbContext context
    )
    {
        _authService = authService;
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated ?? false)
            return RedirectToAction("Index", "Projects");

        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.LoginAsync(model.Email, model.Password);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            // Create claims for the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.Data), // Username
                new Claim(ClaimTypes.Email, model.Email),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : null,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            _logger.LogInformation("User {Email} logged in successfully", model.Email);

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            return RedirectToAction("Index", "Projects");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login attempt for user {Email}", model.Email);
            return HandleError(ex);
        }
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated ?? false)
            return RedirectToAction("Index", "Projects");

        return View(new RegisterViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.RegisterAsync(
                model.Email,
                model.Username,
                model.Password
            );
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            // Update user profile
            await _authService.UpdateUserAsync(
                result.Data,
                model.FirstName,
                model.LastName,
                model.PhoneNumber
            );

            _logger.LogInformation("User {Email} registered successfully", model.Email);
            TempData["Success"] = "Registration successful. Please log in.";

            return RedirectToAction(nameof(Login));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration for user {Email}", model.Email);
            return HandleError(ex);
        }
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        _logger.LogInformation("User logged out");
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        if (User.Identity?.IsAuthenticated ?? false)
            return RedirectToAction("Index", "Projects");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.ResetPasswordAsync(model.Email);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            // Show success message but don't reveal if the email exists
            TempData["Success"] =
                "If your email is registered, you will receive password reset instructions.";
            _logger.LogInformation("Password reset requested for {Email}", model.Email);

            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during password reset request for {Email}", model.Email);
            return HandleError(ex);
        }
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> Profile()
    {
        try
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return RedirectToAction("Login");

            var viewModel = new ProfileViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Created = user.Created,
                LastLogin = user.LastLogin,
                Role = user.Role,
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error loading profile for user {Email}",
                User.FindFirst(ClaimTypes.Email)?.Value
            );
            return HandleError(ex);
        }
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Profile(ProfileViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Users.FindAsync(model.Id);
            if (user == null)
                return RedirectToAction("Login");

            // Update profile information
            var profileResult = await _authService.UpdateUserAsync(
                model.Id,
                model.FirstName,
                model.LastName,
                model.PhoneNumber
            );

            if (!profileResult.Success)
            {
                ModelState.AddModelError(string.Empty, profileResult.Message);
                return View(model);
            }

            // Change password if provided
            if (
                !string.IsNullOrEmpty(model.CurrentPassword)
                && !string.IsNullOrEmpty(model.NewPassword)
            )
            {
                var passwordResult = await _authService.ChangePasswordAsync(
                    model.Id,
                    model.CurrentPassword,
                    model.NewPassword
                );

                if (!passwordResult.Success)
                {
                    ModelState.AddModelError(string.Empty, passwordResult.Message);
                    return View(model);
                }
            }

            TempData["Success"] = "Profile updated successfully";
            return RedirectToAction(nameof(Profile));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error updating profile for user {Email}",
                User.FindFirst(ClaimTypes.Email)?.Value
            );
            return HandleError(ex);
        }
    }
}
