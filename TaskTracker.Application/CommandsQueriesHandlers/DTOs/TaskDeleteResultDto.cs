namespace TaskTracker.Application.CommandsQueriesHandlers.DTOs
{
    public class TaskDeleteResultDto
    {
        public Guid TaskId { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
