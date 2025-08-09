using TaskTracker.Shared.Common;
using TaskTrackerUI.Interfaces;

namespace TaskTrackerUI.Services
{
    public class ActiveTasksService(ITaskService taskService) : IActiveTaskService
    {
        private readonly ITaskService _taskService = taskService;

        private List<TaskItemDTO> Tasks { get; set; } = [];

        public async Task LoadActiveTasks()
        {
            Tasks = [.. (await _taskService.GetAllActiveAsync())];
        }

        public IReadOnlyList<TaskItemDTO> GetActiveTasks()
        {
            return Tasks;
        }
    }
}
