namespace TaskTracker.Application.Tasks.Commands
{
    //Bu command, Task'ın öncelik seviyesini değiştirmek için kullanılır. ID parametresi Task'ın benzersiz kimliğini temsil eder ve NewPriorityLevel parametresi, yeni öncelik seviyesini belirtir.
    public class ChangePriorityCommand
    {
        public Guid Id { get; set; }
        public int NewPriorityLevel { get; set; }
    }
}
