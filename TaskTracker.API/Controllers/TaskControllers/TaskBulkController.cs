using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Commands.Handlers;
using TaskTracker.Application.Tasks.Commands;

namespace TaskTracker.API.Controllers.TaskControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskBulkController(BulkDeleteTasksCommandHandler bulkDelete) : ControllerBase
    {
        private readonly BulkDeleteTasksCommandHandler _bulkDelete = bulkDelete;

        [HttpDelete("bulk-delete")]
        public async Task<IActionResult> BulkDelete([FromBody] List<Guid> taskIds)
        {
            if (taskIds == null || taskIds.Count == 0)
                return BadRequest(new { error = "Geçersiz istek. Silinecek görev ID'leri boş olamaz." });

            var command = new BulkDeleteTasksCommand { TaskIds = taskIds };
            var result = await _bulkDelete.HandleAsync(command);

            if (result.TotalDeleted == 0)
            {
                return BadRequest(new
                {
                    error = "Hiçbir görev silinemedi.",
                    totalRequested = result.TotalRequested,
                    totalDeleted = result.TotalDeleted,
                    failedTasks = result.FailedTasks
                });
            }

            // Bazı görevler silinemediyse ama bazıları silindiyse, yine de 200 OK döneriz.
            return Ok(new
            {
                message = result.FailedTasks.Count != 0
                    ? $"{result.TotalDeleted} görev silindi, {result.FailedTasks.Count} görev silinemedi."
                    : $"{result.TotalDeleted} görev başarıyla silindi.",
                totalRequested = result.TotalRequested,
                totalDeleted = result.TotalDeleted,
                failedTasks = result.FailedTasks.Count != 0 ? result.FailedTasks : null
            });
        }
    }
}
