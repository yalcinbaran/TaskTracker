using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Domain.ValueObjects;
using TaskTracker.SharedKernel.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks.Commands.Handlers
{
    public class ChangeStateCommandHandler(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<(OperationResult Result, Guid UpdatedId)> HandleAsync(ChangeStateCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);

            var task = await _taskRepository.GetByIdAsync(command.Id);
            ArgumentNullException.ThrowIfNull(task);

            var newstate = TaskState.FromLevel(command.NewStateLevel);

            task.ChangeState(newstate);

            try
            {
               var (result, updateId) =  await _taskRepository.UpdateAsync(task);
                if (!result.IsSuccess)
                {
                    return (OperationResult.Fail($"Görev güncellenemedi: {result.Message ?? "Bilinmeyen hata"}"),command.Id);
                }
                return (OperationResult.Ok("Görev başarıyla tamamlandı."), updateId);
            }
            catch (Exception ex)
            {
                return (OperationResult.Fail($"Görev güncelleme sırasında hata meydana geldi: {ex.Message}"),command.Id);
            }
        }
    }
}
