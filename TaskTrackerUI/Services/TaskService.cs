using TaskTracker.Shared.Common;
using TaskTrackerUI.Interfaces;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Services
{
    public class TaskService(IHttpClientFactory httpClient) : ITaskService
    {
        private readonly IHttpClientFactory _httpClient = httpClient;

        public async Task<IEnumerable<TaskItemDTO>> GetAllTasksAsync(Guid userId)
        {
            var client = _httpClient.CreateClient("ApiClient");
            var result = await client.GetFromJsonAsync<IEnumerable<TaskItemDTO>>($"api/Task/GetAllTasks?userId={userId}");
            return result!;
        }

        public async Task<ApiResponse<Guid?>> CreateTaskAsync(TaskModel model)
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

        public async Task<ApiResponse<TaskDeleteResult>> DeleteTaskAsync(Guid id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id.ToString(), nameof(id));
            var client = _httpClient.CreateClient("ApiClient");
            var response = await client.DeleteAsync($"api/Task/DeleteTask/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<TaskDeleteResult>>();
                return result!;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error creating task: {response.ReasonPhrase}, Details: {error}");
            }
        }

        public async Task<ApiResponse<BulkDeleteResult>> BulkDeleteAsync(IEnumerable<Guid> ids)
        {
            ArgumentNullException.ThrowIfNull(ids, nameof(ids));
            var client = _httpClient.CreateClient("ApiClient");
            var response = await client.PostAsJsonAsync("api/TaskBulk/BulkDelete", ids);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<BulkDeleteResult>>();
                return result!;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error bulk deleting tasks: {response.ReasonPhrase}, Details: {error}");
            }
        }

        public async Task<IEnumerable<TaskItemDTO>> GetTasksByStateAsync(int stateLevel)
        {
            ArgumentNullException.ThrowIfNull(stateLevel, nameof(stateLevel));
            if (stateLevel < 0)
                throw new ArgumentOutOfRangeException(nameof(stateLevel), "Geçersiz state seviyesi.");
            var client = _httpClient.CreateClient("ApiClient");
            var response = await client.GetFromJsonAsync<IEnumerable<TaskItemDTO>>($"api/Task/TasksByStateLevel?statelevel={stateLevel}");
            return response == null ? throw new HttpRequestException($"No tasks found for state level {stateLevel}") : response!;
        }



        public async Task<ApiResponse<OperationResult>> UpdateTaskAsync(UpdateTaskModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            var client = _httpClient.CreateClient("ApiClient");

            var response = await client.PutAsJsonAsync("api/Task/UpdateTask", model);

            var operationResult = await response.Content.ReadFromJsonAsync<OperationResult>();

            var apiResponse = new ApiResponse<OperationResult>
            {
                Success = response.IsSuccessStatusCode,
                Message = operationResult?.Message ?? (response.IsSuccessStatusCode
                    ? "Görev başarıyla güncellendi."
                    : "Görev güncellenemedi."),
                Data = operationResult ?? OperationResult.Fail("Bilinmeyen hata.")
            };

            return (apiResponse);
        }

        public async Task<IEnumerable<TaskItemDTO>> GetAllActiveAsync(Guid userId)
        {
            var client = _httpClient.CreateClient("ApiClient");

            var url = $"api/Task/GetActiveTasks?userId={userId}";

            var response = await client.GetFromJsonAsync<IEnumerable<TaskItemDTO>>(url);

            return response!;
        }

        public Task<IEnumerable<TaskItemDTO>> GetAllOverDueAsync(DateTime date, Guid userId)
        {
            var client = _httpClient.CreateClient("ApiClient");

            var response = client.GetFromJsonAsync<IEnumerable<TaskItemDTO>>($"api/TaskSearch/GetOverDueTasks?date={date:yyyy-MM-dd}&userId={userId}");

            return response!;
        }

        public Task<IEnumerable<TaskItemDTO>> GetAllCompletedAsync(Guid userId)
        {
            var cient = _httpClient.CreateClient("ApiClient");

            var response = cient.GetFromJsonAsync<IEnumerable<TaskItemDTO>>($"api/Task/GetCompletedTasks?userId={userId}");

            return response!;
        }

        public Task<IEnumerable<TaskItemDTO>> GetAllCanceledAsync(Guid userId)
        {
            var client = _httpClient.CreateClient("ApiClient");

            var response = client.GetFromJsonAsync<IEnumerable<TaskItemDTO>>($"api/Task/GetCanceledTasks?userId={userId}");

            return response!;
        }
    }
}
