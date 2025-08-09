using TaskTracker.Shared.Common;

namespace TaskTrackerUI.Interfaces
{
    public interface IOverdueTasksService
    {
        Task LoadOverdueTasks(DateTime date, Guid userId);
        List<TaskItemDTO> GetOverdueTasks();
        void ClearOverdueTasks();
    }
}
