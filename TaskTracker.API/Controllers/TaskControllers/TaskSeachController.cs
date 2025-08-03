using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Tasks.Queries;
using TaskTracker.Application.Tasks.Queries.Handlers;

namespace TaskTracker.API.Controllers.TaskControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskSeachController(SearchTasksQueryHandler searchTasksHandler,
                                     GetOverdueTasksQueryHandler overdueTasksHandler) : ControllerBase
    {
        private readonly SearchTasksQueryHandler _searchTasksHandler = searchTasksHandler;
        private readonly GetOverdueTasksQueryHandler _overdueTasksHandler = overdueTasksHandler;

        /// Anahtar kelimeye göre görevleri arar.
        [HttpGet("search")]
        public async Task<IActionResult> SearchTasks([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { error = "Anahtar kelime boş olamaz." });

            var result = await _searchTasksHandler.HandleAsync(new SearchTasksQuery { Keyword = keyword });
            return Ok(result);
        }

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
