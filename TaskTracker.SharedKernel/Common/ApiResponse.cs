using System.Text.Json.Serialization;

namespace TaskTracker.SharedKernel.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public ApiResponse()
        {
            
        }

        private ApiResponse(bool success, string message, T? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> SuccessResponse(T data, string message) =>
            new ApiResponse<T>(true, message, data);

        public static ApiResponse<T> FailResponse(string message) =>
            new ApiResponse<T>(false, message, default);
    }

}
