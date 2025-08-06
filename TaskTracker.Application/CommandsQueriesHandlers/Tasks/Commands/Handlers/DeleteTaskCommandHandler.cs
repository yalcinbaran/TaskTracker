using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Shared.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks.Commands.Handlers
{
    public class DeleteTaskCommandHandler(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<TaskDeleteResult> HandleAsync(DeleteTaskCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);

            try
            {
                // Silme öncesi task detaylarını al
                var task = await _taskRepository.GetByIdAsync(command.Id);
                if (task is null)
                    return TaskDeleteResult.Fail(command.Id, "Görev bulunamadı.");

                var result = await _taskRepository.DeleteAsync(command.Id);
                if (!result.Success)
                    return TaskDeleteResult.Fail(command.Id, result.Message);

                return TaskDeleteResult.Ok(task.Id, task.Title!, task.DueDate);
            }
            catch (Exception ex)
            {
                return TaskDeleteResult.Fail(command.Id, $"Görev silinirken hata oluştu: {ex.Message}");
            }
        }
    }
}
