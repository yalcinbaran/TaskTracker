using TaskTracker.Shared.Common;

namespace TaskTrackerUI.Interfaces
{
    public interface ICanceledTasksService
    {
        Task LoadCanceledTasks(Guid userId);
        IReadOnlyList<TaskItemDTO> GetCanceledTasks();
        void ClearCanceledTasks();
    }
}
