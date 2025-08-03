namespace TaskTracker.Application.Tasks.Commands
{
    //Bu command, bir görevi silmek için kullanılır. Id parametresi, silinecek görevin benzersiz kimliğini temsil eder.
    public class DeleteTaskCommand
    {
        public Guid Id { get; set; }
    }
}
