using TaskTracker.Application.Mappings;
using TaskTracker.Application.Tasks.Queries;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Shared.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries.Handlers
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
