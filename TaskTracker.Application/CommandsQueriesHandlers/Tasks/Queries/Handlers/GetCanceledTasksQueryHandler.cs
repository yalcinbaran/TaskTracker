using TaskTracker.Application.Mappings;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Shared.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries.Handlers
{
    public class GetCanceledTasksQueryHandler(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<IEnumerable<TaskItemDTO>> HandleAsync(GetCanceledTasksQuery query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            var tasks = await _taskRepository.GetAllCanceledTasksAsync(query.UserId);

            var dtos = tasks.ToDtoList();

            return dtos!;
        }
    }
}
