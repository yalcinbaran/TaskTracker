using TaskTracker.Application.CommandsQueriesHandlers.DTOs;
using TaskTracker.Application.Mappings;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Shared.Common;

namespace TaskTracker.Application.Tasks.Queries.Handlers
{
    public class GetOverdueTasksQueryHandler(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<IEnumerable<TaskItemDTO>> HandleAsync(GetOverdueTasksQuery query)
        {
            ArgumentNullException.ThrowIfNull(query);
            DateTime date = query.ReferenceDate!.Value.Date;
            var tasks = await _taskRepository.GetAllOverDueTasks(date);
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
