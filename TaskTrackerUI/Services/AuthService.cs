using TaskTracker.SharedKernel.Common;
using TaskTrackerUI.Interfaces;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Services
{
    public class AuthService(IHttpClientFactory httpClient) : IAuthService
    {
        private readonly IHttpClientFactory _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        public async Task<ApiResponse<T>> LoginAsync<T>(LoginModel login)
        {
            var client = _httpClient.CreateClient("ApiClient");
            var response = await client.PostAsJsonAsync("api/User/login", login);
            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadFromJsonAsync<ApiResponse<UserDTO>>();
                return ApiResponse<T>.FailResponse(message!.Message);
            }
            var content = await response.Content.ReadFromJsonAsync<ApiResponse<UserDTO>>();
            if (content is null)
                return ApiResponse<T>.FailResponse("API'den geçerli bir yanıt alınamadı.");
            try
            {
                if (typeof(T) == typeof(UserDTO))
                {
                    return new ApiResponse<T>
                    {
                        Success = content!.Success,
                        Message = content.Message,
                        Data = (T)(object)content.Data!
                    };
                }
                else
                {
                    return ApiResponse<T>.FailResponse("Beklenmeyen veri tipi.");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse<T>.FailResponse("Cevap işlenemedi: " + ex.Message);
            }
        }

        public async Task<ApiResponse<T>> RegisterAsync<T>(RegisterModel register)
        {
            var client = _httpClient.CreateClient("ApiClient");
            var response = await client.PostAsJsonAsync("api/User/CreateUser", register);
            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadFromJsonAsync<ApiResponse<UserDTO>>();
                return ApiResponse<T>.FailResponse(message!.Message);
            }
            var content = await response.Content.ReadFromJsonAsync<ApiResponse<UserDTO>>();
            if (content is null)
                return ApiResponse<T>.FailResponse("API'den geçerli bir yanıt alınamadı.");
            try
            {
                if (typeof(T) == typeof(UserDTO))
                {
                    return new ApiResponse<T>
                    {
                        Success = content!.Success,
                        Message = content.Message,
                        Data = (T)(object)content.Data!
                    };
                }
                else
                {
                    return ApiResponse<T>.FailResponse("Beklenmeyen veri tipi.");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse<T>.FailResponse("Cevap işlenemedi: " + ex.Message);
            }
        }
    }
}
