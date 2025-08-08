using TaskTracker.Domain.Entities;
using TaskTracker.Shared.Common;

namespace TaskTracker.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<(OperationResult Result, Guid CreatedId)> AddAsync(TaskItem task);
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskItem>> GetTasksByStateAsync(int taskStateLevel);
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<IEnumerable<TaskItem>> GetAllOverDueTasks(DateTime date);
        Task<IEnumerable<TaskItem>> GetAllActiveTasksAsync(DateTime date);
        Task<IEnumerable<TaskItem>> GetAlCompletedTasksAsync();
        Task<(OperationResult Result, Guid UpdatedId)> UpdateAsync(TaskItem task);
        Task<OperationResult> DeleteAsync(Guid id);
        Task<List<TaskDeleteResult>> DeleteRangeAsync(IEnumerable<Guid> ids);
    }
}
