using TaskTracker.Shared.Common;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<UserDTO>> LoginAsync(LoginModel login);
        Task<ApiResponse<T>> RegisterAsync<T>(RegisterModel register);
    }
}
