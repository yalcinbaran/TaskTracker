using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Domain.ValueObjects;
using TaskTracker.Shared.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks.Commands.Handlers
{
    public class UpdateTaskCommandHandler(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<OperationResult> HandleAsync(UpdateTaskCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);
            var task = await _taskRepository.GetByIdAsync(command.Id);
            if (task == null)
            {
                return (OperationResult.Fail("Görev bulunamadı."));
            }

            try
            {
                task.Update(
                    command.Description!,
                    command.DueDate,
                    Priority.FromLevel(command.PriorityLevel),
                    TaskState.FromLevel(command.TaskStateLevel),
                    command.UserId
                );
                var (Result, UpdatedId) = await _taskRepository.UpdateAsync(task);
                if (!Result.IsSuccess)
                {
                    return (OperationResult.Fail($"Görev güncelleme sırasında hata meydana geldi: {Result.Message ?? "Bilinmeyen hata"}"));
                }
                return (OperationResult.Ok("Görev başarıyla güncellendi."));
            }
            catch (Exception ex)
            {
                return (OperationResult.Fail($"Görev güncelleme sırasında hata meydana geldi: {ex.Message}"));
            }
        }
    }
}
