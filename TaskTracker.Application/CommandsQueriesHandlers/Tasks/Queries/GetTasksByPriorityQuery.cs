namespace TaskTracker.Application.Tasks.Queries
{
    //Bu Query, Task'ların öncelik durumuna göre filtrelenerek veritabanından getirilip UI tarafında göstermek için kullanılacak.
    public class GetTasksByPriorityQuery
    {
        public int PriorityLevel { get; set; }
    }
}
