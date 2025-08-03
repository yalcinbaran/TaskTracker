using TaskTracker.Application.CommandsQueriesHandlers.DTOs;
using TaskTracker.Application.Mappings;
using TaskTracker.Application.Tasks.Queries;
using TaskTracker.Domain.Interfaces;
using TaskTracker.SharedKernel.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries.Handlers
{
    public class GetTaskByIdQueryHandler(ITaskRepository taskRepository, IUserRepository userRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<TaskItemDTO> HandleAsync(GetTaskByIdQuery query)
        {            
            ArgumentNullException.ThrowIfNull(query);
            var task = await _taskRepository.GetByIdAsync(query.Id) ?? throw new KeyNotFoundException("Görev bulunamadı.");
            var dto = task.ToDto()!;

            var user = await _userRepository.GetByIdAsync(task.UserId);
            dto.User = user?.ToDto();

            return dto;
        }
    }
}
