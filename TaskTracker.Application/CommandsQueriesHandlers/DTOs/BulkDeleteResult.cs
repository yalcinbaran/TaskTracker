namespace TaskTracker.Application.CommandsQueriesHandlers.DTOs
{
    public record BulkDeleteResult(int TotalRequested, int TotalDeleted, List<FailedTaskInfo> FailedTasks)
    {
        public bool IsSuccess => FailedTasks.Count == 0;

        public static BulkDeleteResult Ok(int totalRequested, int totalDeleted)
            => new BulkDeleteResult(totalRequested, totalDeleted, new List<FailedTaskInfo>());

        public static BulkDeleteResult Fail(int totalRequested, List<FailedTaskInfo> failedTasks)
            => new BulkDeleteResult(totalRequested, totalRequested - failedTasks.Count, failedTasks);
    }
}
