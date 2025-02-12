using Pro.Structure.Core.Models;

namespace Pro.Structure.Core.Interfaces;

public interface IAuthService
{
    Task<ServiceResponse<string>> LoginAsync(string email, string password);
    Task<ServiceResponse<int>> RegisterAsync(string email, string username, string password);
    Task<ServiceResponse<bool>> ChangePasswordAsync(
        int userId,
        string currentPassword,
        string newPassword
    );
    Task<ServiceResponse<bool>> ResetPasswordAsync(string email);
    Task<ServiceResponse<bool>> ValidateResetTokenAsync(string email, string token);
    Task<ServiceResponse<bool>> LockoutUserAsync(string email);
    Task<ServiceResponse<bool>> UnlockUserAsync(string email);
    Task<ServiceResponse<bool>> UpdateUserAsync(
        int userId,
        string? firstName,
        string? lastName,
        string? phoneNumber
    );
}
