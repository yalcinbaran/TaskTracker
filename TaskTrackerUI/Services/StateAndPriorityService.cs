using TaskTracker.Shared.Common;
using TaskTrackerUI.Interfaces;

namespace TaskTrackerUI.Services
{
    public class StateAndPriorityService(ITaskApiService taskApiService) : IStateAndPriorityService
    {
        private readonly ITaskApiService _taskApiService = taskApiService;
        private List<StateDto> _states = [];
        private List<PriorityDto> _priorities = [];
        public async Task LoadStatesAsync()
        {
            _states = [..await _taskApiService.GetStatesAsync()];
        }
        public async Task LoadPrioritiesAsync()
        {
            _priorities = [..await _taskApiService.GetPrioritiesAsync()];
        }

        public IReadOnlyList<StateDto> GetStates()
        {
            return _states;
        }
        public IReadOnlyList<PriorityDto> GetPriorities()
        {
            return _priorities;
        }
    }
}
