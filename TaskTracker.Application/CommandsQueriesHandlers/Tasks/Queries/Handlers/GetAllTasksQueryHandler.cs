using TaskTracker.Application.Mappings;
using TaskTracker.Domain.Interfaces;
using TaskTracker.SharedKernel.Common;

namespace TaskTracker.Application.Tasks.Queries.Handlers
{
    public class GetAllTasksQueryHandler(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<IEnumerable<TaskItemDTO?>> HandleAsync(GetAllTasksQuery query)
        {
            var tasks = await _taskRepository.GetAllAsync();
            return tasks.ToDtoList();
        }
    }
}
