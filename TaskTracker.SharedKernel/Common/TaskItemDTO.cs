namespace TaskTracker.Shared.Common
{
    public class TaskItemDTO
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }

        // Priority'yi basit tip olarak tutuyoruz
        public string? PriorityName { get; set; }

        // Priority'yi basit tip olarak tutuyoruz
        public string? TaskStateName { get; set; }

        public Guid UserId { get; set; }
    }
}
