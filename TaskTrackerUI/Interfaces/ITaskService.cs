using TaskTracker.Shared.Common;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDTO>> GetAllTasksAsync();
        Task<IEnumerable<TaskItemDTO>> GetTasksByStateAsync(int stateLevel);
        Task<ApiResponse<Guid?>> CreateTaskAsync(TaskModel model);
        Task<ApiResponse<TaskDeleteResult>> DeleteTaskAsync(Guid id);
        Task<ApiResponse<BulkDeleteResult>> BulkDeleteAsync(IEnumerable<Guid> ids);
        Task<ApiResponse<OperationResult>> UpdateTaskAsync(UpdateTaskModel model);
    }
}
