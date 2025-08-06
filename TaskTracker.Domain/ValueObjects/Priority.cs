using TaskTracker.Shared;

namespace TaskTracker.Domain.ValueObjects
{
    public sealed class Priority : ValueObject
    {
        public static readonly Priority Low = new("Düşük", 1);
        public static readonly Priority Medium = new("Orta", 2);
        public static readonly Priority High = new("Yüksek", 3);
        public static readonly Priority VeryHigh = new("Acil", 4);

        public string Name { get; }
        public int Level { get; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private Priority() { } // EF Core için gerekli
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private Priority(string name, int level)
        {
            Name = name;
            Level = level;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Level;
        }

        public static Priority FromName(string name)
        {
            return name.ToLower() switch
            {
                "low" => Low,
                "medium" => Medium,
                "high" => High,
                "veryhigh" => VeryHigh,
                _ => throw new ArgumentException($"Unknown priority: {name}")
            };
        }

        public static Priority FromLevel(int level)
        {
            return level switch
            {
                1 => Low,
                2 => Medium,
                3 => High,
                4 => VeryHigh,
                _ => throw new ArgumentException("Invalid priority level", nameof(level))
            };
        }

        public override string ToString() => Name;
    }
}
