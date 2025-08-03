using TaskTracker.Application.CommandsQueriesHandlers.DTOs;
using TaskTracker.Application.Mappings;
using TaskTracker.Domain.Interfaces;

namespace TaskTracker.Application.Tasks.Queries.Handlers
{
    public class SearchTasksQueryHandler(ITaskRepository taskRepository, IUserRepository userRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<IEnumerable<TaskItemDTO>> HandleAsync(SearchTasksQuery query)
        {
            ArgumentNullException.ThrowIfNull(query);
            var tasks = await _taskRepository.GetTasksBySerachQuery(query.Keyword);

            var dtos = new List<TaskItemDTO>();
            foreach (var task in tasks)
            {
                var dto = task.ToDto()!;
                var user = await _userRepository.GetByIdAsync(task.UserId);
                dto.User = user?.ToDto();
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
