using TaskTracker.Shared.Common;

namespace TaskTrackerUI.Interfaces
{
    public interface ICanceledTasksService
    {
        Task LoadCanceledTasks();
        IReadOnlyList<TaskItemDTO> GetCanceledTasks();
    }
}
