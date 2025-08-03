using TaskTracker.Application.CommandsQueriesHandlers.DTOs;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.ValueObjects;

namespace TaskTracker.Application.Mappings
{
    public static class TaskItemMappings
    {
        // Domain -> DTO dönüşümü
        public static TaskItemDTO? ToDto(this TaskItem task)
        {
            if (task == null) return null;

            return new TaskItemDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                PriorityName = task.Priority!.Name,
                TaskStateName = task.TaskState!.Name,
                UserId = task.UserId
            };
        }

        // DTO -> Domain dönüşümü
        public static TaskItem? ToDomain(this TaskItemDTO dto)
        {
            if (dto == null) return null;

            var priority = Priority.FromName(dto.PriorityName!);
            var state = TaskState.FromName(dto.TaskStateName!);
            return new TaskItem(
                dto.Title ?? string.Empty,
                dto.Description ?? string.Empty,
                dto.DueDate,
                priority,
                state,
                dto.UserId
            );
        }

        // Domain listesi -> DTO listesi dönüşümü
        public static IEnumerable<TaskItemDTO?> ToDtoList(this IEnumerable<TaskItem> tasks)
        {
            return tasks?.Select(t => t.ToDto()) ?? [];
        }
    }
}
