using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Domain.ValueObjects;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks.Commands.Handlers
{
    public class CreateTaskCommandHandler(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<(OperationResult Result, Guid? CreatedId)> HandleAsync(CreateTaskCommand command)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));

            var task = new TaskItem(
                command.Title,
                command.Description,
                command.DueDate,
                Priority.FromLevel(command.PriorityLevel),
                TaskState.FromLevel(command.StateLevel),
                command.UserId
            );

            try
            {
                var (result, createdId) = await _taskRepository.AddAsync(task);

                if (!result.Success)
                {
                    return (OperationResult.Fail("Görev oluşturulması sırasında hata meydana geldi: " + (result.Message ?? "Bilinmeyen hata")), null);
                }

                return (OperationResult.Ok("Görev başarıyla oluşturuldu."), createdId);
            }
            catch (Exception ex)
            {
                return (OperationResult.Fail($"Görev oluşturulamadı: {ex.Message}"), null);
            }
        }
    }
}
