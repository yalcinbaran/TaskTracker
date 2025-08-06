namespace TaskTracker.Application.Tasks.Commands
{
    // Bu command, var olan bir TaskItem'ı (görevi) güncellemek için kullanılır.
    public class UpdateTaskCommand
    {
        public Guid Id { get; set; }  // Güncellenecek Task'ın ID'si
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }

        public int PriorityLevel { get; set; }
        public int TaskStateLevel { get; set; }
        public Guid UserId { get; set; }  // Görevi güncelleyen kullanıcının ID'si
    }
}
