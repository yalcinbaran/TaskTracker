using TaskTracker.Domain.ValueObjects;
using TaskTracker.Shared.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks
{
    public class GetStatesQueryHandler
    {
        public IEnumerable<StateDto> Handle()
        {
            return
            [
                new() { Name = TaskState.Pending.Name, Level = TaskState.Pending.Level },
                new() { Name = TaskState.InProgress.Name, Level = TaskState.InProgress.Level },
                new() { Name = TaskState.Completed.Name, Level = TaskState.Completed.Level },
                new() { Name = TaskState.Cancelled.Name, Level = TaskState.Cancelled.Level }
            ];
        }
    }
}
