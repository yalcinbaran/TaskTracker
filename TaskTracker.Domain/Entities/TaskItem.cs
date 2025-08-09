using TaskTracker.Domain.Interfaces;
using TaskTracker.Domain.OwnedTypes;
using TaskTracker.Domain.ValueObjects;
using TaskTracker.Shared;

namespace TaskTracker.Domain.Entities
{
    public class TaskItem : BaseEntity, IAggregateRoot
    {
        public string? Title { get; private set; }
        public string? Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public Priority? Priority { get; private set; }
        public TaskState? TaskState { get; private set; }
        public Guid UserId { get; private set; }

        private TaskItem() { } // EF için

        public TaskItem(string title, string description, DateTime dueDate, Priority priority, TaskState state, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Görev başlığı boş olamaz.", nameof(title));
            if (dueDate.Date < DateTime.Today)
                throw new ArgumentException("Teslim tarihi geçmiş bir görev oluşturulamaz.", nameof(dueDate));

            if (description is null)
                throw new ArgumentNullException(nameof(description), "Görev açıklaması boş olamaz.");
            if(title.Length > 100)
                throw new ArgumentException("Görev başlığı 200 karakterden uzun olamaz.", nameof(title));
            if(description.Length > 1000)
                throw new ArgumentException("Görev açıklaması 1000 karakterden uzun olamaz.", nameof(description));
            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            TaskState = state;
            UserId = userId;
        }

        public void Update(string description, DateTime dueDate, Priority priority, TaskState state, Guid userId)
        {
            if (description is null)
                throw new ArgumentNullException(nameof(description), "Görev açıklaması boş olamaz.");
            if (description.Length > 1000)
                throw new ArgumentException("Görev açıklaması 1000 karakterden uzun olamaz.", nameof(description));
            if (dueDate.Date < DateTime.Today)
                throw new ArgumentException("Teslim tarihi geçmiş bir görev oluşturulamaz.", nameof(dueDate));

            Description = description;
            DueDate = dueDate;
            Priority = priority;
            TaskState = state;
            UserId = userId;
        }

        public void ChangePriority(Priority newPriority)
        {
            ArgumentNullException.ThrowIfNull(newPriority);

            if (Priority == newPriority)
                return; // Değişiklik yok

            if (DueDate < DateTime.UtcNow)
                throw new InvalidOperationException("Teslim tarihi geçmiş bir görevin önceliği değiştirilemez.");

            Priority = newPriority;
        }

        public void ChangeState(TaskState newState)
        {
            ArgumentNullException.ThrowIfNull(newState);
            if (TaskState == newState)
                return; // Değişiklik yok

            // Teslim tarihi geçmişse durum değiştirilemez
            if (DueDate < DateTime.UtcNow)
                throw new InvalidOperationException("Teslim tarihi geçmiş bir görevin durumu değiştirilemez.");

            TaskState = newState;
        }
    }
}
