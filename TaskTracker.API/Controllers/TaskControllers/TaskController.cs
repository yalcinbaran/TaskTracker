using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Commands.Handlers;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries.Handlers;
using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Application.Tasks.Queries;
using TaskTracker.Shared.Common;

namespace TaskTracker.API.Controllers.TaskControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(CreateTaskCommandHandler createTask,
                                UpdateTaskCommandHandler updateTask,
                                DeleteTaskCommandHandler deleteTask,
                                GetTaskByIdQueryHandler getTaskById,
                                GetAllTasksQueryHandler getAllTasks,
                                GetActiveTasksQueryHandler getActiveTasks,
                                GetPriorityQueryHandler getPriorities,
                                GetStatesQueryHandler getStates,
                                GetTasksByStateQueryHandler getTasksByState) : ControllerBase
    {
        private readonly CreateTaskCommandHandler _createTask = createTask;
        private readonly UpdateTaskCommandHandler _updateTask = updateTask;
        private readonly DeleteTaskCommandHandler _deleteTask = deleteTask;
        private readonly GetTaskByIdQueryHandler _getTaskById = getTaskById;
        private readonly GetAllTasksQueryHandler _getAllTasks = getAllTasks;
        private readonly GetActiveTasksQueryHandler _getActiveTasks = getActiveTasks;
        private readonly GetPriorityQueryHandler _getPriorities = getPriorities;
        private readonly GetStatesQueryHandler _getStates = getStates;
        private readonly GetTasksByStateQueryHandler _getTasksByState = getTasksByState;

        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            if (command == null)
                return BadRequest("Geçersiz istek.");

            var (result, createdId) = await _createTask.HandleAsync(command);

            if (result.Success && createdId.HasValue)
            {
                // CreatedAtAction ile oluşturulan kaynağın GetById endpoint'ine link veriyoruz.
                return CreatedAtAction(nameof(GetTaskById), new { id = createdId.Value }, new { message = result.Message, result.Success });
            }
            else
            {
                return BadRequest(new { message = result.Message });
            }
        }

        [HttpPut("UpdateTask")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskCommand command)
        {
            if (command == null)
                return BadRequest(OperationResult.Fail("Geçersiz istek."));

            var result = await _updateTask.HandleAsync(command);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpDelete("DeleteTask/{id:guid}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            if (id == Guid.Empty)
            {
                var fail = TaskDeleteResult.Fail(id, "Geçersiz görev ID.");
                return BadRequest(new ApiResponse<TaskDeleteResult>
                {
                    Success = false,
                    Message = fail.ErrorMessage!,
                    Data = fail
                });
            }

            var command = new DeleteTaskCommand { Id = id };
            var result = await _deleteTask.HandleAsync(command);

            var response = new ApiResponse<TaskDeleteResult>
            {
                Success = result.Success,
                Message = result.Success ? "Görev başarıyla silindi." : result.ErrorMessage!,
                Data = result
            };

            return result.Success ? Ok(response) : BadRequest(response);
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

        [HttpGet("GetActiveTasks")]
        public async Task<IActionResult> GetActiveTasks([FromQuery] DateTime date)
        {
            var taskDtos = await _getActiveTasks.HandleAsync(new GetActiveTasksQuery() { DueDate = date });
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

        [HttpGet("TasksByStateLevel")]
        public async Task<IActionResult> GetTasksByStateLevel([FromQuery] int statelevel)
        {
            if (statelevel < 0)
                return BadRequest("Geçersiz state level.");

            var query = new GetTasksByStateQuery { TaskStateLevel = statelevel };
            try
            {
                var tasks = await _getTasksByState.HandleAsync(query);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
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
