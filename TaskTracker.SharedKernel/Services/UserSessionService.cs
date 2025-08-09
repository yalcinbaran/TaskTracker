using TaskTracker.Shared.Interfaces;
using TaskTracker.Shared.Common;

namespace TaskTracker.Shared.Services
{
    public class UserSessionService : IUserSessionService
    {
        public UserSession CurrentSession { get; private set; } = new();

        public void ClearSession()
        {
            CurrentSession = new UserSession();
        }

        public void SetSession(UserDTO user)
        {
            CurrentSession = new UserSession
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = $"{user.Name} {user.Surname}"
            };
        }
    }
}
