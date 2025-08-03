namespace TaskTracker.Domain.ValueObjects
{
    public record OperationResult(bool Success, string? Message = null, string? ErrorCode = null)
    {
        public DateTime Timestamp { get; } = DateTime.UtcNow;

        public bool IsSuccess => Success;

        public static OperationResult Ok(string? message = null)
            => new OperationResult(true, message);

        public static OperationResult Fail(string message, string? errorCode = null)
            => new OperationResult(false, message, errorCode);
    }
}
