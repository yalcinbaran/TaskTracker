namespace TaskTracker.Shared.Common
{
    //Bu sınıf, silme işlemleri için kullanılacak. Eğer birden çok görev silinirken bazı görevler silinemiyorsa, bu sınıfın örneği oluşturulacak ve hata mesajı ile birlikte TaskId'si döndürülecek.
    public record TaskDeleteResult(Guid TaskId, string TaskTitle, DateTime DueDate, bool Success, string? ErrorMessage = null)
    {         
        public static TaskDeleteResult Ok(Guid taskId, string taskTitle, DateTime dueDate)
            => new TaskDeleteResult(taskId, taskTitle, dueDate, true);
        public static TaskDeleteResult Fail(Guid taskId, string? errorMessage = null)
            => new TaskDeleteResult(taskId, string.Empty, DateTime.MinValue, false, errorMessage);
    }
}
