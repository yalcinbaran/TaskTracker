using TaskTracker.SharedKernel.Common;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Interfaces
{
    public interface IUserSessionService
    {
        UserSession CurrentSession { get; }
        void SetSession(UserDTO user);
        void ClearSession();
    }
}
