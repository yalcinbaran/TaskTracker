using TaskTracker.Shared.Common;

namespace TaskTrackerUI.Interfaces
{
    public interface ICompletedTasksService
    {
        Task LoadCompletedTasks(Guid userId);
        IReadOnlyList<TaskItemDTO> GetCompletedTasks();
        void ClearCompletedTasks();
    }
}
