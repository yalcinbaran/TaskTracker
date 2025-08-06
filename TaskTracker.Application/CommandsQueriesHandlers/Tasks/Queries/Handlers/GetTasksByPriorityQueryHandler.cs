using TaskTracker.Application.Mappings;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Shared.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries.Handlers
{
    public class GetTasksByPriorityQueryHandler(ITaskRepository taskRepository, IUserRepository userRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<IEnumerable<TaskItemDTO>> HandleAsync(int priorityLevel)
        {
            var tasks = await _taskRepository.GetTasksByPriorityAsync(priorityLevel);

            var dtos = new List<TaskItemDTO>();
            foreach (var task in tasks)
            {
                var dto = task.ToDto()!;
                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
