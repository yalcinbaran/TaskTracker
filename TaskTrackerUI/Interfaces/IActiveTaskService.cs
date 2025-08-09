using TaskTracker.Shared.Common;

namespace TaskTrackerUI.Interfaces
{
    public interface IActiveTaskService
    {
        Task LoadActiveTasks(Guid userId);
        IReadOnlyList<TaskItemDTO> GetActiveTasks();
        void ClearActiveTasks();
    }
}
