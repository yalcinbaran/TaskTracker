namespace TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries
{
    //Bu Query, zamanı geçmiş görevleri almak için kullanılır. Parametre handler içerisinde vermek yerine burada verildi. Test ortamları için pratiklik sağlayacak.
    public class GetOverdueTasksQuery
    {
        public DateTime? ReferenceDate { get; set; } = DateTime.UtcNow;
    }
}
