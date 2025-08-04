using TaskTracker.Domain.ValueObjects;
using TaskTracker.Shared.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks
{
    public class GetPriorityQueryHandler
    {
        public IEnumerable<PriorityDto> Handle()
        {
            return
            [
                new() { Name = Priority.Low.Name, Level = Priority.Low.Level },
                new() { Name = Priority.Medium.Name, Level = Priority.Medium.Level },
                new() { Name = Priority.High.Name, Level = Priority.High.Level },
                new() { Name = Priority.VeryHigh.Name, Level = Priority.VeryHigh.Level }
            ];
        }
    }
}
