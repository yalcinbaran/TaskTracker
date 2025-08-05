using TaskTracker.SharedKernel;

namespace TaskTracker.Domain.ValueObjects
{
    public sealed class TaskState : ValueObject
    {
        public static readonly TaskState Cancelled = new("İptal edildi", 1);
        public static readonly TaskState Pending = new("Beklemede", 2);
        public static readonly TaskState InProgress = new("Devam ediyor", 3);
        public static readonly TaskState Completed = new("Tamamlandı", 4);

        public string Name { get; }
        public int Level { get; }

        private TaskState(string name, int level)
        {
            Name = name;
            Level = level;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private TaskState() { } // EF Core için gerekli
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public static TaskState FromName(string name)
        {
            return name.ToLower() switch
            {
                "İptal edildi" => Cancelled,
                "Beklemede" => Pending,
                "Devam ediyor" => InProgress,
                "Tamamlandı" => Completed,
                _ => throw new ArgumentException($"Invalid task state: {name}")
            };
        }

        public static TaskState FromLevel(int level)
        {
            return level switch
            {
                1 => Cancelled,
                2 => Pending,
                3 => InProgress,
                4 => Completed,
                _ => throw new ArgumentException($"Invalid task state code: {level}")
            };
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Level;
        }

        public override string ToString() => Name;
    }
}
