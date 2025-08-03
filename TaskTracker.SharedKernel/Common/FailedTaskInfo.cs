namespace TaskTracker.Application.CommandsQueriesHandlers.DTOs
{
    //Bu sınıf, başarısız olan Task'ların bilgilerini tutmak için kullanılacak. Özellikle, Task'ın ID'si, başlığı, son tarihi ve hata mesajı gibi bilgileri içerecek. Liste ya da tekil olarak işlemlerin sonucu olarak kullanılabilir.
    public class FailedTaskInfo(Guid taskId, string tasktitle, string reason, DateTime duedate)
    {
        public Guid TaskId { get; set; } = taskId;
        public string TaskTitle { get; set; } = tasktitle;
        public string Reason { get; set; } = reason;
        public DateTime DueDate { get; set; } = duedate;
    }
}
