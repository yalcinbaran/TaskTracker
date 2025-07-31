using TaskTracker.Domain.Entities;

namespace TaskTracker.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskItem task);
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(Guid id);
        Task DeleteRange(IEnumerable<Guid> ids);
    }
}
