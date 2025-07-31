using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.DTOs;
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
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                DueDate = task.DueDate,
                PriorityLevel = task.Priority.Level
            };
        }

        // DTO -> Domain dönüşümü
        public static TaskItem? ToDomain(this TaskItemDTO dto)
        {
            if (dto == null) return null;

            var priority = Priority.FromLevel(dto.PriorityLevel);

            return new TaskItem(
                dto.Title ?? string.Empty,
                dto.Description ?? string.Empty,
                dto.IsCompleted,
                dto.DueDate,
                priority
            );
        }

        // Domain listesi -> DTO listesi dönüşümü
        public static IEnumerable<TaskItemDTO?> ToDtoList(this IEnumerable<TaskItem> tasks)
        {
            return tasks?.Select(t => t.ToDto()) ?? [];
        }
    }
}
