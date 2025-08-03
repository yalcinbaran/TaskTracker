using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Application.Tasks.Commands.Handlers;
using TaskTracker.Application.Tasks.Queries.Handlers;

namespace TaskTracker.API.Controllers.TaskControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskPriorityController(ChangePriorityCommandHandler changePriorityHandler,
                                        GetTasksByPriorityQueryHandler getTasksByPriorityHandler) : ControllerBase
    {
        private readonly ChangePriorityCommandHandler _changePriorityHandler = changePriorityHandler;
        private readonly GetTasksByPriorityQueryHandler _getTasksByPriorityHandler= getTasksByPriorityHandler;


        // Görev önceliğini değiştirme
        [HttpPut("change-priority")]
        public async Task<IActionResult> ChangePriority([FromBody] ChangePriorityCommand command)
        {
            var result = await _changePriorityHandler.HandleAsync(command);
            if (result.Success)
                return Ok(new { message = result.Message });

            return BadRequest(new { error = result.Message });
        }

        // Belirli öncelik seviyesindeki görevleri getirme
        [HttpGet("{priorityLevel:int}")]
        public async Task<IActionResult> GetTasksByPriority(int priorityLevel)
        {
            var result = await _getTasksByPriorityHandler.HandleAsync(priorityLevel);
            return Ok(result);
        }
    }
}
