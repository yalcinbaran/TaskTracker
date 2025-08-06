using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Commands.Handlers;
using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Shared.Common;

namespace TaskTracker.API.Controllers.TaskControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskBulkController(BulkDeleteTasksCommandHandler bulkDelete) : ControllerBase
    {
        private readonly BulkDeleteTasksCommandHandler _bulkDelete = bulkDelete;

        [HttpPost("BulkDelete")]
        public async Task<IActionResult> BulkDelete([FromBody] List<Guid> taskIds)
        {
            if (taskIds == null || taskIds.Count == 0)
                return BadRequest(ApiResponse<BulkDeleteResult>.FailResponse("Geçersiz istek. Silinecek görev ID'leri boş olamaz."));

            var command = new BulkDeleteTasksCommand { TaskIds = taskIds };
            var result = await _bulkDelete.HandleAsync(command);

            if (result.IsTotalFailure)
            {
                return BadRequest(ApiResponse<BulkDeleteResult>.FailResponse("Hiçbir görev silinemedi.", result));
            }

            if (result.IsPartialSuccess)
            {
                return Ok(ApiResponse<BulkDeleteResult>.PartialSuccessResponse(result,
                    $"{result.TotalDeleted} görev silindi, {result.FailedTasks.Count} görev silinemedi."));
            }

            return Ok(ApiResponse<BulkDeleteResult>.SuccessResponse(result, $"{result.TotalDeleted} görev başarıyla silindi."));
        }

    }
}
