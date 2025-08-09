using TaskTracker.Shared.Common;
using TaskTrackerUI.Services;

namespace TaskTrackerUI.Interfaces
{
    public interface IUserSessionService
    {
        void SetSession(UserDTO user);
        UserSession GetSession();
        void ClearSession();
    }
}
