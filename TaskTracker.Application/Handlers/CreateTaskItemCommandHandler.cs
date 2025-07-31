using TaskTracker.Application.Commands;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Domain.ValueObjects;

namespace TaskTracker.Application.Handlers
{
    // TaskTracker.Application/Handlers/CreateTaskItemCommandHandler.cs
    public class CreateTaskItemCommandHandler
    {
        private readonly ITaskRepository _taskRepository;

        public CreateTaskItemCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Guid> Handle(CreateTaskItemCommand request)
        {
            var task = new TaskItem(
                request.Title,
                request.Description,
                request.IsCompleted,
                request.DueDate,
                Priority.FromLevel(request.Priority.Level)
            );

            await _taskRepository.AddAsync(task);

            return task.Id;
        }
    }

}
