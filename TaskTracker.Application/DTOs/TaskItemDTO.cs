using TaskTracker.Domain.ValueObjects;

namespace TaskTracker.Application.DTOs
{
    public class TaskItemDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }

        // Priority'yi basit tip olarak tutuyoruz
        public int PriorityLevel { get; set; }
    }
}
