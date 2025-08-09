using TaskTracker.Shared.Common;

namespace TaskTrackerUI.Interfaces
{
    public interface IStateAndPriorityService
    {
        Task LoadStatesAsync();
        Task LoadPrioritiesAsync();
        IReadOnlyList<StateDto> GetStates();
        IReadOnlyList<PriorityDto> GetPriorities();
    }
}
