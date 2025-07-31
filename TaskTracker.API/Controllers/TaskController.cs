using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.DTOs;
using TaskTracker.Application.Mappings;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Domain.ValueObjects;

namespace TaskTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(ITaskRepository taskRepository) : ControllerBase
    {
        private readonly ITaskRepository _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskRepository.GetAllAsync();
            var dtos = tasks.ToDtoList();
            return Ok(dtos);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            var dto = task?.ToDto();
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskItemDTO dto)
        {
            // DTO'daki PriorityLevel'dan domain Priority nesnesi oluşturuyoruz
            var priority = Priority.FromLevel(dto.PriorityLevel);

            var task = new TaskItem(
                dto.Title ?? throw new ArgumentNullException(nameof(dto.Title)),
                dto.Description ?? string.Empty,
                dto.IsCompleted,
                dto.DueDate,
                priority
            );

            await _taskRepository.AddAsync(task);
            var anotherDTO = task.ToDto();
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, anotherDTO);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TaskItemDTO dto)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null)
                return NotFound();

            var priority = Priority.FromLevel(dto.PriorityLevel);

            existingTask.Update(
                dto.Title ?? existingTask.Title,
                dto.Description ?? existingTask.Description,
                dto.IsCompleted,
                dto.DueDate,
                priority
            );

            await _taskRepository.UpdateAsync(existingTask);
            return NoContent();
        }


        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _taskRepository.DeleteAsync(id);
            return NoContent();
        }

        // BULK DELETE
        [HttpPost("bulk-delete")]
        public async Task<IActionResult> BulkDelete([FromBody] List<Guid> ids)
        {
            if (ids == null || ids.Count == 0)
                return BadRequest("List of IDs cannot be empty.");

            await _taskRepository.DeleteRange(ids);
            return NoContent();
        }
    }
}
