using TaskTracker.Shared.Common;
using TaskTrackerUI.Interfaces;

namespace TaskTrackerUI.Services
{
    public class CompletedTasksService(ITaskService taskService) : ICompletedTasksService
    {
        private readonly ITaskService _taskService = taskService;
        private List<TaskItemDTO> Tasks { get; set; } = [];
        public async Task LoadCompletedTasks(Guid userId)
        {
            Tasks = [.. (await _taskService.GetAllCompletedAsync(userId))];
        }
        public IReadOnlyList<TaskItemDTO> GetCompletedTasks()
        {
            return Tasks;
        }

        public void ClearCompletedTasks()
        {
            Tasks.Clear();
        }
    }
}
