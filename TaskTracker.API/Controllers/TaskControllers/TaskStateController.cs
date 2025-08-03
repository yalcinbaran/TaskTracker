using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Commands.Handlers;
using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Application.Tasks.Queries;
using TaskTracker.Application.Tasks.Queries.Handlers;

namespace TaskTracker.API.Controllers.TaskControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskStateController(ChangeStateCommandHandler changeStateHandler,
                                     GetTasksByStateQueryHandler getTasksByStateHandler) : ControllerBase
    {
        private readonly ChangeStateCommandHandler _changeStateHandler = changeStateHandler;
        private readonly GetTasksByStateQueryHandler _getTasksByStateHandler = getTasksByStateHandler;

        // 🔄 1. Görev durumunu değiştirme
        [HttpPut("change-state")]
        public async Task<IActionResult> ChangeState([FromBody] ChangeStateCommand command)
        {
            var (result, Id) = await _changeStateHandler.HandleAsync(command);
            if (result.Success)
                return Ok(new { message = result.Message, Id });

            return BadRequest(new { error = result.Message });
        }

        // 📄 2. Belirli bir durumdaki görevleri listeleme
        [HttpGet("{stateLevel}")]
        public async Task<IActionResult> GetTasksByState(int stateLevel)
        {
            var query = new ChangeStateCommand { NewStateLevel = stateLevel };
            var result = await _changeStateHandler.HandleAsync(query);
            return Ok(result);
        }
    }

}
