using TaskTracker.Application.Mappings;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Shared.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries.Handlers
{
    public class GetActiveTasksQueryHandler(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        public async Task<IEnumerable<TaskItemDTO>> HandleAsync(GetActiveTasksQuery query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            var date = DateTime.UtcNow.Date;

            var tasks = await _taskRepository.GetAllActiveTasksAsync(date, query.UserId);

            var dtos = tasks.ToDtoList();

            return dtos!;
        }
    }
}
