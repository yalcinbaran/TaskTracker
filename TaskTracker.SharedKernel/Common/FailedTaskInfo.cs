namespace TaskTracker.Shared.Common
{
    //Bu sınıf, başarısız olan Task'ların bilgilerini tutmak için kullanılacak. Özellikle, Task'ın ID'si, başlığı, son tarihi ve hata mesajı gibi bilgileri içerecek. Liste ya da tekil olarak işlemlerin sonucu olarak kullanılabilir.
    public class FailedTaskInfo(Guid taskId, string? title, string error, DateTime? dueDate = null)
    {
        public Guid TaskId { get; set; } = taskId;
        public string? TaskTitle { get; set; } = title;
        public string ErrorMessage { get; set; } = error;
        public DateTime? DueDate { get; set; } = dueDate;
    }
}
