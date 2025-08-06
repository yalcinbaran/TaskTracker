namespace TaskTracker.Shared.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public bool PartialSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string? message = null) =>
            new() { Success = true, Data = data, Message = message };

        public static ApiResponse<T> FailResponse(string error, T? data = default) =>
            new() { Success = false, Error = error, Data = data };

        public static ApiResponse<T> PartialSuccessResponse(T data, string message) =>
            new() { Success = true, PartialSuccess = true, Data = data, Message = message };
    }

}
