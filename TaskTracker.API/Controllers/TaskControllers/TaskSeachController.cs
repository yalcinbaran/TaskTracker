using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries.Handlers;
using TaskTracker.Application.Tasks.Queries;

namespace TaskTracker.API.Controllers.TaskControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskSeachController(GetOverdueTasksQueryHandler overdueTasksHandler) : ControllerBase
    {
        private readonly GetOverdueTasksQueryHandler _overdueTasksHandler = overdueTasksHandler;


        /// Belirli bir tarihe kadar gecikmiş görevleri getirir.
        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueTasks([FromQuery] DateTime? referenceDate)
        {
            var query = new GetOverdueTasksQuery
            {
                ReferenceDate = referenceDate ?? DateTime.UtcNow
            };

            var result = await _overdueTasksHandler.HandleAsync(query);
            return Ok(result);
        }
    }
}
