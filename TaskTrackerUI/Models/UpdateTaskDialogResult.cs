using TaskTracker.Shared.Common;

namespace TaskTrackerUI.Models
{
    public class UpdateTaskDialogResult
    {
        public ApiResponse<OperationResult> Response { get; set; } = default!;
        public int TaskStateLevel { get; set; }
    }
}
