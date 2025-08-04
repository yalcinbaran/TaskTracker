using TaskTracker.Shared.Common;
using TaskTracker.SharedKernel.Common;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDTO>> GetAllTasksAsync();
        Task<ApiResponse<Guid?>> CreateTaskAsync(CreateTaskModel model);
    }
}
