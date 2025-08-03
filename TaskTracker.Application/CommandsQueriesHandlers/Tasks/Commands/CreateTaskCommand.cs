namespace TaskTracker.Application.Tasks.Commands
{
    //Bu command, yeni bir görev oluşturmak için kullanılır.
    public class CreateTaskCommand
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public int PriorityLevel { get; set; }
        public int StateLevel { get; set; }
        public Guid UserId { get; set; }
    }
}
