using TaskTracker.Shared;

namespace TaskTracker.Domain.OwnedTypes
{
    public sealed class Priority : ValueObject
    {
        public static readonly Priority Low = new("Düşük", 1);
        public static readonly Priority Medium = new("Orta", 2);
        public static readonly Priority High = new("Yüksek", 3);
        public static readonly Priority VeryHigh = new("Acil", 4);

        public string Name { get; }
        public int Level { get; }
        private Priority() { } // EF Core için gerekli

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
