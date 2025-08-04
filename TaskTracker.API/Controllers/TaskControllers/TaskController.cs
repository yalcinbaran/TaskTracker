using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Commands.Handlers;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries.Handlers;
using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Application.Tasks.Queries;
using TaskTracker.Application.Tasks.Queries.Handlers;

namespace TaskTracker.API.Controllers.TaskControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(CreateTaskCommandHandler createTask, 
                                UpdateTaskCommandHandler updateTask, 
                                DeleteTaskCommandHandler deleteTask,
                                GetTaskByIdQueryHandler getTaskById,
                                GetAllTasksQueryHandler getAllTasks,
                                GetPriorityQueryHandler getPriorities,
                                GetStatesQueryHandler getStates) : ControllerBase
    {
        private readonly CreateTaskCommandHandler _createTask = createTask;
        private readonly UpdateTaskCommandHandler _updateTask = updateTask;
        private readonly DeleteTaskCommandHandler _deleteTask = deleteTask;
        private readonly GetTaskByIdQueryHandler _getTaskById = getTaskById;
        private readonly GetAllTasksQueryHandler _getAllTasks = getAllTasks;
        private readonly GetPriorityQueryHandler _getPriorities = getPriorities;
        private readonly GetStatesQueryHandler _getStates = getStates;

        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            if (command == null)
                return BadRequest("Geçersiz istek.");

            var (result, createdId) = await _createTask.HandleAsync(command);

            if (result.Success && createdId.HasValue)
            {
                // CreatedAtAction ile oluşturulan kaynağın GetById endpoint'ine link veriyoruz.
                return CreatedAtAction(nameof(GetTaskById), new { id = createdId.Value }, new { message = result.Message , result.Success });
            }
            else
            {
                return BadRequest(new { message = result.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskCommand command)
        {
            if (command == null || id != command.Id)
                return BadRequest("Geçersiz istek.");

            var (result, updatedId) = await _updateTask.HandleAsync(command);
            if (result.IsSuccess)
                return Ok(new { message = result.Message, id = updatedId });
            else
                return BadRequest(new { message = result.Message });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Geçersiz Id.");
            var command = new DeleteTaskCommand { Id = id };
            var result = await _deleteTask.HandleAsync(command);

            if (result.IsSuccess)
                return Ok(new { message = result.Message });
            else
                return BadRequest(new { message = result.Message });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Geçersiz Id.");
            var query = new GetTaskByIdQuery { Id = id };
            try
            {
                var task = await _getTaskById.HandleAsync(query);
                return Ok(task);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Id’si '{id}' olan görev bulunamadı.");
            }
        }

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var taskDtos = await _getAllTasks.HandleAsync(new GetAllTasksQuery());
            return Ok(taskDtos);
        }

        [HttpGet("priorities")]
        public IActionResult GetPriorities()
        {
            try
            {
                var priorities = _getPriorities.Handle();
                return Ok(priorities);
            }
            catch (Exception ex)
            {
                // Detaylı hata mesajı dönüşü (yalnızca geliştirme ortamında yapılmalı)
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpGet("states")]
        public IActionResult GetStates()
        {
            try
            {
                var states = _getStates.Handle();
                return Ok(states);
            }
            catch (Exception ex)
            {
                // Detaylı hata mesajı dönüşü (yalnızca geliştirme ortamında yapılmalı)
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }
    }
}
