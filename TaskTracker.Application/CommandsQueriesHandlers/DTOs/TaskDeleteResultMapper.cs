using TaskTracker.Domain.ValueObjects;

namespace TaskTracker.Application.CommandsQueriesHandlers.DTOs
{
    public static class TaskDeleteResultMapper
    {
        public static TaskDeleteResultDto ToDto(this TaskDeleteResult result) =>
            new()
            {
                TaskId = result.TaskId,
                Success = result.Success,
                ErrorMessage = result.ErrorMessage
            };
    }
}
