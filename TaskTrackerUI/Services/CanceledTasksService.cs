using TaskTracker.Shared.Common;
using TaskTrackerUI.Interfaces;

namespace TaskTrackerUI.Services
{
    public class CanceledTasksService(ITaskService taskService) : ICanceledTasksService
    {
        private readonly ITaskService _taskService = taskService;

        private List<TaskItemDTO> Tasks { get; set; } = [];

        public async Task LoadCanceledTasks(Guid userId)
        {
            Tasks = [.. (await _taskService.GetAllCanceledAsync(userId))];
        }

        public IReadOnlyList<TaskItemDTO> GetCanceledTasks()
        {
            return Tasks;
        }

        public void ClearCanceledTasks()
        {
            Tasks.Clear();
        }
    }
}
