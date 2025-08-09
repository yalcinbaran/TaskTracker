using TaskTracker.Shared.Common;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDTO>> GetAllTasksAsync(Guid userId);
        Task<IEnumerable<TaskItemDTO>> GetAllActiveAsync(Guid userId);
        Task<IEnumerable<TaskItemDTO>> GetAllCompletedAsync(Guid userId);
        Task<IEnumerable<TaskItemDTO>> GetAllCanceledAsync(Guid userId);
        Task<IEnumerable<TaskItemDTO>> GetAllOverDueAsync(DateTime date, Guid userId);
        Task<IEnumerable<TaskItemDTO>> GetTasksByStateAsync(int stateLevel);
        Task<ApiResponse<Guid?>> CreateTaskAsync(TaskModel model);
        Task<ApiResponse<TaskDeleteResult>> DeleteTaskAsync(Guid id);
        Task<ApiResponse<BulkDeleteResult>> BulkDeleteAsync(IEnumerable<Guid> ids);
        Task<ApiResponse<OperationResult>> UpdateTaskAsync(UpdateTaskModel model);
    }
}
