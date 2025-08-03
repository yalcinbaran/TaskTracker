using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Domain.ValueObjects;
using TaskTracker.SharedKernel.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks.Commands.Handlers
{
    public class UpdateTaskCommandHandler(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<(OperationResult Result, Guid UpdatedId)> HandleAsync(UpdateTaskCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);
            var task = await _taskRepository.GetByIdAsync(command.Id);
            if (task == null)
            {
                return (OperationResult.Fail("Görev bulunamadı."), Guid.Empty);
            }

            try
            {
                task.Update(
                    command.Title!,
                    command.Description!,
                    command.DueDate,
                    Priority.FromLevel(command.PriorityLevel),
                    TaskState.FromLevel(command.TaskStateLevel)
                );
                var (result,taskId) = await _taskRepository.UpdateAsync(task);
                if (!result.Success)
                {
                    return (OperationResult.Fail($"Görev güncelleme sırasında hata meydana geldi: {result.Message ?? "Bilinmeyen hata"}"), taskId);
                }
                return (OperationResult.Ok("Görev başarıyla güncellendi."), taskId);
            }
            catch (Exception ex)
            {
                return (OperationResult.Fail($"Görev güncelleme sırasında hata meydana geldi: {ex.Message}"),task.Id);
            }
        }
    }
}
