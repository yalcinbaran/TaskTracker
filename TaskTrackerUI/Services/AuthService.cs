using Microsoft.AspNetCore.Diagnostics;
using TaskTracker.Shared.Common;
using TaskTrackerUI.Interfaces;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Services
{
    public class AuthService(IHttpClientFactory httpClient) : IAuthService
    {
        private readonly IHttpClientFactory _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        public async Task<ApiResponse<UserDTO>> LoginAsync(LoginModel login)
        {
            var client = _httpClient.CreateClient("ApiClient");
            var response = await client.PostAsJsonAsync("api/User/login", login);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadFromJsonAsync<ApiResponse<UserDTO>>();
                return ApiResponse<UserDTO>.FailResponse(errorContent?.Error ?? "Sunucudan hata yanıtı alındı.");
            }

            var content = await response.Content.ReadFromJsonAsync<ApiResponse<UserDTO>>();

            if (content is null || content.Data is null)
            {
                return ApiResponse<UserDTO>.FailResponse("API'den geçerli bir yanıt alınamadı.");
            }

            return content;
        }



        public async Task<ApiResponse<UserDTO>> RegisterAsync(RegisterModel register)
        {
            var client = _httpClient.CreateClient("ApiClient");
            var response = await client.PostAsJsonAsync("api/User/CreateUser", register);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadFromJsonAsync<ApiResponse<UserDTO>>();
                return ApiResponse<UserDTO>.FailResponse(errorContent?.Error ?? "Sunucudan hata yanıtı alındı.");
            }
            var content = await response.Content.ReadFromJsonAsync<ApiResponse<UserDTO>>();
            if (content is null)
                return ApiResponse<UserDTO>.FailResponse("API'den geçerli bir yanıt alınamadı.");
            try
            {
                    return new ApiResponse<UserDTO>
                    {
                        Success = content!.Success,
                        Message = content.Message,
                        Data = content.Data!
                    };
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDTO>.FailResponse("Cevap işlenemedi: " + ex.Message);
            }
        }
    }
}
