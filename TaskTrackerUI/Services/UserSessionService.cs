using Microsoft.AspNetCore.Components;
using TaskTracker.Shared.Common;
using TaskTrackerUI.Interfaces;

namespace TaskTrackerUI.Services
{
    public class UserSessionService(NavigationManager navManager,
                                    IActiveTaskService activeTask,
                                    ICanceledTasksService canceledTasks,
                                    ICompletedTasksService completedTasks,
                                    IOverdueTasksService overdueTasks) : IUserSessionService
    {
        private readonly NavigationManager _navManager = navManager;
        private readonly IActiveTaskService _activeTask = activeTask;
        private readonly ICanceledTasksService _canceledTasks = canceledTasks;
        private readonly ICompletedTasksService _completedTasks = completedTasks;
        private readonly IOverdueTasksService _overdueTasks = overdueTasks;
        private UserSession CurrentSession { get; set; } = new();

        public void ClearSession()
        {
            CurrentSession = new UserSession();
            _activeTask.ClearActiveTasks();
            _canceledTasks.ClearCanceledTasks();
            _completedTasks.ClearCompletedTasks();
            _overdueTasks.ClearOverdueTasks();
            _navManager.NavigateTo("/");
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

        public UserSession GetSession()
        {
            return CurrentSession;
        }
    }
}
