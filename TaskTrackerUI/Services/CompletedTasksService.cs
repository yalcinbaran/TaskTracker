using TaskTracker.Shared.Common;
using TaskTrackerUI.Interfaces;

namespace TaskTrackerUI.Services
{
    public class CompletedTasksService(ITaskService taskService) : ICompletedTasksService
    {
        private readonly ITaskService _taskService = taskService;
        private List<TaskItemDTO> Tasks { get; set; } = [];
        public async Task LoadCompletedTasks()
        {
            Tasks = [.. (await _taskService.GetAllCompletedAsync())];
        }
        public IReadOnlyList<TaskItemDTO> GetCompletedTasks()
        {
            return Tasks;
        }
    }
}
