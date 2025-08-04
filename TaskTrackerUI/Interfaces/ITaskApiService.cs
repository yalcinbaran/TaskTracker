using TaskTracker.Shared.Common;

namespace TaskTrackerUI.Interfaces
{
    public interface ITaskApiService
    {
        Task<IEnumerable<PriorityDto>> GetPrioritiesAsync();
        Task<IEnumerable<StateDto>> GetStatesAsync();
    }
}
