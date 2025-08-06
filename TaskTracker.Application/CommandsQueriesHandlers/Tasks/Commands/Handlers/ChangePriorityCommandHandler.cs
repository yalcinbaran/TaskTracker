using TaskTracker.Domain.Interfaces;
using TaskTracker.Domain.ValueObjects;
using TaskTracker.Shared.Common;

namespace TaskTracker.Application.Tasks.Commands.Handlers
{
    public class ChangePriorityCommandHandler(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<OperationResult> HandleAsync(ChangePriorityCommand command)
        {
            var task = await _taskRepository.GetByIdAsync(command.Id);
            if (task == null)
            {
                return OperationResult.Fail("Görev bulunamadı.");
            }

            var priority = Priority.FromLevel(command.NewPriorityLevel);

            task.ChangePriority(priority);

            try
            {
                await _taskRepository.UpdateAsync(task);
                return OperationResult.Ok("Görev önceliği başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"Görev önceliği güncellenemedi: {ex.Message}");
            }            
        }
    }
}
