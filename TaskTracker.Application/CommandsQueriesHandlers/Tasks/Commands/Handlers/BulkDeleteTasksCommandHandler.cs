using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Shared.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks.Commands.Handlers
{
    //Bu Handler, birden çok Task'ı silmek için kullanılacak. HandleAsync metoduna gelen command, silinecek Task'ların ID'lerini içerecek ve Repository içerisindeki DeleteRangeAsync metodunu çağırarak bu Task'ları veritabanından silecek.
    //Repository Interface'i, Dependency Injection ile parametre olarak sınıfa alınır ve private readonly field olarak saklanır. HandleAsync metodu içerisinde private readonly field üzerinden DeleteRangeAsync metodu çağrılır.
    public class BulkDeleteTasksCommandHandler(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<BulkDeleteResult> HandleAsync(BulkDeleteTasksCommand command)
        {
            if (command.TaskIds == null || command.TaskIds.Count == 0)
                return BulkDeleteResult.Ok(0, 0);

            var deleteResults = await _taskRepository.DeleteRangeAsync(command.TaskIds);

            var failedTasks = deleteResults
                .Where(r => !r.Success)
                .Select(r => new FailedTaskInfo(
                    r.TaskId,
                    r.TaskTitle,
                    !string.IsNullOrWhiteSpace(r.ErrorMessage)
                        ? $"Bir hata meydana geldi: {r.ErrorMessage}"
                        : "Bilinmeyen hata",
                    r.DueDate))
                .ToList();

            return failedTasks.Count != 0
                ? BulkDeleteResult.Fail(command.TaskIds.Count, failedTasks)
                : BulkDeleteResult.Ok(command.TaskIds.Count, deleteResults.Count(r => r.Success));
        }
    }
}
