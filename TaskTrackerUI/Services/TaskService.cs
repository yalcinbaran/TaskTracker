using TaskTracker.Shared.Common;
using TaskTracker.SharedKernel.Common;
using TaskTrackerUI.Interfaces;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Services
{
    public class TaskService(IHttpClientFactory httpClient) : ITaskService
    {
        private readonly IHttpClientFactory _httpClient = httpClient;

        public async Task<ApiResponse<Guid?>> CreateTaskAsync(CreateTaskModel model)
        {
            ArgumentNullException.ThrowIfNull(model, nameof(model));

            var client = _httpClient.CreateClient("ApiClient");

            var response = await client.PostAsJsonAsync("api/Task/CreateTask", model);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<Guid?>>();
                return result!;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error creating task: {response.ReasonPhrase}, Details: {error}");
            }
        }


        public Task<IEnumerable<TaskItemDTO>> GetAllTasksAsync()
        {
            var client = _httpClient.CreateClient("ApiClient");
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return client.GetFromJsonAsync<IEnumerable<TaskItemDTO>>("api/Task/GetAllTasks");
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
    }
}
