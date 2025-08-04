using TaskTracker.Shared.Common;
using TaskTrackerUI.Interfaces;

namespace TaskTrackerUI.Services
{
    public class TaskApiService(IHttpClientFactory httpClientFactory) : ITaskApiService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<IEnumerable<PriorityDto>> GetPrioritiesAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<PriorityDto>>("api/Task/priorities");
                if (response == null)
                    throw new HttpRequestException("Failed to retrieve priorities.");
                return response;
            }
            catch (Exception ex)
            {
                // Log veya kullanıcıya mesaj göster
                throw new HttpRequestException("API çağrısı sırasında hata oluştu: " + ex.Message, ex);
            }
        }

        public async Task<IEnumerable<StateDto>> GetStatesAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            try
            {
                var response = await client.GetFromJsonAsync<IEnumerable<StateDto>>("api/Task/states");
                if (response == null)
                    throw new HttpRequestException("Failed to retrieve states.");
                return response;
            }
            catch (Exception ex)
            {
                // Log veya kullanıcıya mesaj göster
                throw new HttpRequestException("API çağrısı sırasında hata oluştu: " + ex.Message, ex);
            }
        }
    }
}
