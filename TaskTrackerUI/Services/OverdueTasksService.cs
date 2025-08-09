using TaskTracker.Shared.Common;
using TaskTrackerUI.Interfaces;

namespace TaskTrackerUI.Services
{
    public class OverdueTasksService(ITaskService taskService) : IOverdueTasksService
    {
        private readonly ITaskService _taskService = taskService;
        private List<TaskItemDTO> Tasks = [];

        public async Task LoadOverdueTasks(DateTime date, Guid userId)
        {
            Tasks = [.. (await _taskService.GetAllOverDueAsync(date, userId))];
        }

        public List<TaskItemDTO> GetOverdueTasks()
        {
            return Tasks;
        }

        public void ClearOverdueTasks()
        {
            Tasks.Clear();
        }
    }
}
