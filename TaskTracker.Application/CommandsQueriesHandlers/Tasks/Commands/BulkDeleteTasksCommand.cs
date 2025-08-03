namespace TaskTracker.Application.Tasks.Commands
{
    //Bu command, birden fazla görevi silmek için kullanılır.
    public class BulkDeleteTasksCommand
    {
        public List<Guid>? TaskIds { get; set; }
    }
}
