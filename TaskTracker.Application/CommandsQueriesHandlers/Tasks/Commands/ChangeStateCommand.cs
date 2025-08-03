namespace TaskTracker.Application.Tasks.Commands
{
    //Bu command, açık olan bir görevin durumunu güncellemek için kullanılır. Id parametresi, görevin benzersiz kimliğini temsil eder.
    public class ChangeStateCommand
    {
        public Guid Id { get; set; }
        public int NewStateLevel { get; set; }
    }
}
