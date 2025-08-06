namespace TaskTracker.Shared.Common
{
    public class BulkDeleteResult
    {
        public int TotalRequested { get; set; }
        public int TotalDeleted { get; set; }
        public List<FailedTaskInfo> FailedTasks { get; set; } = [];

        public bool IsPartialSuccess => FailedTasks.Count != 0;
        public bool IsTotalFailure => TotalDeleted == 0;

        public static BulkDeleteResult Ok(int totalRequested, int totalDeleted) =>
            new()
            {
                TotalRequested = totalRequested,
                TotalDeleted = totalDeleted,
                FailedTasks = []
            };

        public static BulkDeleteResult Fail(int totalRequested, List<FailedTaskInfo> failedTasks) =>
            new()
            {
                TotalRequested = totalRequested,
                TotalDeleted = totalRequested - failedTasks.Count,
                FailedTasks = failedTasks
            };
    }
}
