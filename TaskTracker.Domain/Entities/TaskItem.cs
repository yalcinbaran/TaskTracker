using TaskTracker.Domain.Enums;
using TaskTracker.Domain.ValueObjects;
using TaskTracker.SharedKernel;

namespace TaskTracker.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        public string? Title { get; private set; }
        public string? Description { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTime DueDate { get; private set; }
        public Priority? Priority { get; private set; }

        private TaskItem() { } // EF için

        public TaskItem(string title, string description, bool iscompleted, DateTime dueDate, Priority priority)
        {
            Title = title;
            Description = description;
            IsCompleted = iscompleted;
            DueDate = dueDate;
            Priority = priority;
        }

        public void Update(string title, string description, bool iscompleted, DateTime dueDate, Priority priority)
        {
            Title = title;
            Description = description;
            IsCompleted = iscompleted;
            DueDate = dueDate;
            Priority = priority;
        }
    }
}
