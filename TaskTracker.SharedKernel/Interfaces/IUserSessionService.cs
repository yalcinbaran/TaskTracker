using TaskTracker.Shared.Common;

namespace TaskTracker.Shared.Interfaces
{
    public interface IUserSessionService
    {
        UserSession CurrentSession { get; }
        void SetSession(UserDTO user);
        void ClearSession();
    }
}
