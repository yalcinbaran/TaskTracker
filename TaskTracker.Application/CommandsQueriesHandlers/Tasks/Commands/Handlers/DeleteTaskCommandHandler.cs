using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Domain.Interfaces;
using TaskTracker.SharedKernel.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks.Commands.Handlers
{
    public class DeleteTaskCommandHandler(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<OperationResult> HandleAsync(DeleteTaskCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);

            try
            {
                var result = await _taskRepository.DeleteAsync(command.Id);

                if (!result.Success)
                {
                    return OperationResult.Fail($"Görev silinemedi: {result.Message ?? "Bilinmeyen hata"}");
                }

                return OperationResult.Ok("Görev başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"Görev silinirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
