using TaskTracker.Shared.Common;

namespace TaskTrackerUI.Interfaces
{
    public interface IActiveTaskService
    {
        Task LoadActiveTasks();
        IReadOnlyList<TaskItemDTO> GetActiveTasks();
    }
}
