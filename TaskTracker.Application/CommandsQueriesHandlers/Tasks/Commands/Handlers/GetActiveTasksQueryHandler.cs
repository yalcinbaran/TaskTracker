using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries;
using TaskTracker.Application.Mappings;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Shared.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks.Commands.Handlers
{
    public class GetActiveTasksQueryHandler(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        public async Task<IEnumerable<TaskItemDTO>> HandleAsync(GetActiveTasksQuery query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));
            var tasks = await _taskRepository.GetAllActiveTasks(query.DueDate);
            var dtos = tasks.Select(task => task.ToDto()).ToList();
            return dtos;
        }
    }
}
