using TaskTracker.Application.Mappings;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Shared.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries.Handlers
{
    public class GetOverdueTasksQueryHandler(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<IEnumerable<TaskItemDTO>> HandleAsync(GetOverdueTasksQuery query)
        {
            ArgumentNullException.ThrowIfNull(query);

            var tasks = await _taskRepository.GetAllOverDueTasks(query.ReferenceDate!.Value);
            if (tasks == null || !tasks.Any())
            {
                return [];
            }
            else
            {
                return tasks.ToDtoList()!;
            }
        }
    }
}
