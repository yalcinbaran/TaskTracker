using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries.Handlers;

namespace TaskTracker.API.Controllers.TaskControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskSearchController(GetOverdueTasksQueryHandler overdueTasksHandler) : ControllerBase
    {
        private readonly GetOverdueTasksQueryHandler _overdueTasksHandler = overdueTasksHandler;


        /// Belirli bir tarihe kadar gecikmiş görevleri getirir.

        [HttpGet("GetOverDueTasks")]
        public async Task<IActionResult> GetOverdueTasks([FromQuery] DateTime date)
        {
            if (date == default)
                return BadRequest("Geçersiz tarih.");
            var query = new GetOverdueTasksQuery { ReferenceDate = date };
            try
            {
                var overdueTasks = await _overdueTasksHandler.HandleAsync(query);
                return Ok(overdueTasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }
    }
}
