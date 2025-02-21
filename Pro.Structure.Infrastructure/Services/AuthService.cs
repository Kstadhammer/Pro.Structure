using System.Security.Cryptography;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pro.Structure.Core.Entities;
using Pro.Structure.Core.Interfaces;
using Pro.Structure.Core.Models;
using Pro.Structure.Infrastructure.Data;

namespace Pro.Structure.Infrastructure.Services;

/// <summary>
/// Implementation of authentication and authorization services.
/// This implementation was developed with significant AI assistance for:
/// - Security best practices
/// - Password hashing and verification
/// - Account lockout mechanisms
/// - Error handling and logging patterns
/// </summary>
public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AuthService> _logger;

    public AuthService(ApplicationDbContext context, ILogger<AuthService> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Authenticates a user and manages login attempts.
    /// Implementation assisted by AI for secure password verification and account lockout logic.
    /// </summary>
    public async Task<ServiceResponse<string>> LoginAsync(string email, string password)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == email || u.Username == email
            );

            if (user == null)
                return ServiceResponse<string>.Fail("Invalid email or password");

            if (!user.IsActive)
                return ServiceResponse<string>.Fail("Account is disabled");

            if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.UtcNow)
                return ServiceResponse<string>.Fail($"Account is locked until {user.LockoutEnd}");

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                user.FailedLoginAttempts++;

                // Lock account after 5 failed attempts
                if (user.FailedLoginAttempts >= 5)
                {
                    user.LockoutEnd = DateTime.UtcNow.AddMinutes(15);
                    await _context.SaveChangesAsync();
                    return ServiceResponse<string>.Fail(
                        "Account locked due to too many failed attempts"
                    );
                }

                await _context.SaveChangesAsync();
                return ServiceResponse<string>.Fail("Invalid email or password");
            }

            // Reset failed attempts on successful login
            user.FailedLoginAttempts = 0;
            user.LastLogin = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return ServiceResponse<string>.Ok(user.Username, "Login successful");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for user {Email}", email);
            return ServiceResponse<string>.Fail("An error occurred during login");
        }
    }

    /// <summary>
    /// Registers a new user account.
    /// Implementation assisted by AI for secure password hashing and duplicate checking.
    /// </summary>
    public async Task<ServiceResponse<int>> RegisterAsync(
        string email,
        string username,
        string password
    )
    {
        try
        {
            if (await _context.Users.AnyAsync(u => u.Email == email))
                return ServiceResponse<int>.Fail("Email already registered");

            if (await _context.Users.AnyAsync(u => u.Username == username))
                return ServiceResponse<int>.Fail("Username already taken");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Email = email,
                Username = username,
                PasswordHash = passwordHash,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return ServiceResponse<int>.Ok(user.Id, "Registration successful");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration for user {Email}", email);
            return ServiceResponse<int>.Fail("An error occurred during registration");
        }
    }

    /// <summary>
    /// Changes a user's password.
    /// Implementation assisted by AI for secure password verification and update process.
    /// </summary>
    public async Task<ServiceResponse<bool>> ChangePasswordAsync(
        int userId,
        string currentPassword,
        string newPassword
    )
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return ServiceResponse<bool>.Fail("User not found");

            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                return ServiceResponse<bool>.Fail("Current password is incorrect");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.Modified = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return ServiceResponse<bool>.Ok(true, "Password changed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error changing password for user {UserId}", userId);
            return ServiceResponse<bool>.Fail("An error occurred while changing password");
        }
    }

    /// <summary>
    /// Initiates a password reset process.
    /// Implementation assisted by AI for secure token generation and email handling.
    /// </summary>
    public async Task<ServiceResponse<bool>> ResetPasswordAsync(string email)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return ServiceResponse<bool>.Ok(true); // Don't reveal if email exists

            // Generate reset token (implement secure token generation)
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

            // TODO: Store token securely and send email

            return ServiceResponse<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error requesting password reset for {Email}", email);
            return ServiceResponse<bool>.Fail("An error occurred while processing your request");
        }
    }

    /// <summary>
    /// Validates a password reset token.
    /// TODO: Implementation pending, will be assisted by AI for secure token validation.
    /// </summary>
    public async Task<ServiceResponse<bool>> ValidateResetTokenAsync(string email, string token)
    {
        // TODO: Implement token validation
        return ServiceResponse<bool>.Fail("Not implemented");
    }

    /// <summary>
    /// Locks a user account.
    /// Implementation assisted by AI for proper account state management.
    /// </summary>
    public async Task<ServiceResponse<bool>> LockoutUserAsync(string email)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return ServiceResponse<bool>.Fail("User not found");

            user.IsActive = false;
            user.Modified = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return ServiceResponse<bool>.Ok(true, "User account locked");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error locking user account {Email}", email);
            return ServiceResponse<bool>.Fail("An error occurred while locking the account");
        }
    }

    /// <summary>
    /// Unlocks a user account.
    /// Implementation assisted by AI for proper account state reset.
    /// </summary>
    public async Task<ServiceResponse<bool>> UnlockUserAsync(string email)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return ServiceResponse<bool>.Fail("User not found");

            user.IsActive = true;
            user.FailedLoginAttempts = 0;
            user.LockoutEnd = null;
            user.Modified = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return ServiceResponse<bool>.Ok(true, "User account unlocked");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error unlocking user account {Email}", email);
            return ServiceResponse<bool>.Fail("An error occurred while unlocking the account");
        }
    }

    /// <summary>
    /// Updates a user's profile information.
    /// Implementation assisted by AI for secure data handling and validation.
    /// </summary>
    public async Task<ServiceResponse<bool>> UpdateUserAsync(
        int userId,
        string? firstName,
        string? lastName,
        string? phoneNumber
    )
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return ServiceResponse<bool>.Fail("User not found");

            user.FirstName = firstName;
            user.LastName = lastName;
            user.PhoneNumber = phoneNumber;
            user.Modified = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return ServiceResponse<bool>.Ok(true, "Profile updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating profile for user {UserId}", userId);
            return ServiceResponse<bool>.Fail("An error occurred while updating the profile");
        }
    }
}
