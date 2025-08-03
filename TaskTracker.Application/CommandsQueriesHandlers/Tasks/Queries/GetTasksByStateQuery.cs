namespace TaskTracker.Application.Tasks.Queries
{
    //Bu Query, Task'ların tamamlanma durumuna göre filtrelenerek veritabanından getirilip UI tarafında göstermek için kullanılacak.
    public class GetTasksByStateQuery
    {
        public int TaskStateLevel { get; set; }
    }
}
