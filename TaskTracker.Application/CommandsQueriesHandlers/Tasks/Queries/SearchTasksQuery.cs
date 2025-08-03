namespace TaskTracker.Application.Tasks.Queries
{
    //Bu Query, bir arama fonksiyonu için kullanılacak. Görev başlığını veya açıklamasını aramak için kullanılabilir.
    public class SearchTasksQuery
    {
        public string Keyword { get; set; } = string.Empty;
    }
}
