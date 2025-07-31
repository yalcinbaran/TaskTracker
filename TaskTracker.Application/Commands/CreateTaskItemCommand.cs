using TaskTracker.Domain.ValueObjects;

namespace TaskTracker.Application.Commands
{
    public class CreateTaskItemCommand
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public Priority? Priority { get; set; }
    }
}
