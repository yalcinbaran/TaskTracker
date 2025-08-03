using TaskTracker.Application.CommandsQueriesHandlers.DTOs;
using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Domain.Interfaces;

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
            {
                return BulkDeleteResult.Ok(0, 0);
            }

            var deleteResults = await _taskRepository.DeleteRangeAsync(command.TaskIds);

            var failedTasks = deleteResults
                .Where(r => !r.Success)
                .Select(r => new FailedTaskInfo(
                    r.TaskId,
                    r.TaskTitle,
                    $"Bir hata meydana geldi: {r.ErrorMessage}" ?? "Bilinmeyen hata",
                    r.DueDate))
                .ToList();

            var totalDeleted = deleteResults.Count(r => r.Success);

            if (failedTasks.Count != 0)
            {
                return BulkDeleteResult.Fail(command.TaskIds.Count, failedTasks);
            }

            return BulkDeleteResult.Ok(command.TaskIds.Count, totalDeleted);
        }
    }
}
