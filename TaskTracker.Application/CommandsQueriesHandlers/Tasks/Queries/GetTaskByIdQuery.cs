namespace TaskTracker.Application.Tasks.Queries
{
    //Bu Query, Task'ı Id'sine göre veri tabanından getirmek ve UI tarafında göstermek için kullanılır.
    public class GetTaskByIdQuery
    {
        public Guid Id { get; set; }
    }
}
