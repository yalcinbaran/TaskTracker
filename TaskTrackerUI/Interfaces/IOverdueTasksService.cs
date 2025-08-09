using TaskTracker.Shared.Common;

namespace TaskTrackerUI.Interfaces
{
    public interface IOverdueTasksService
    {
        Task LoadOverdueTasks(DateTime date);
        List<TaskItemDTO> GetOverdueTasks();
    }
}
