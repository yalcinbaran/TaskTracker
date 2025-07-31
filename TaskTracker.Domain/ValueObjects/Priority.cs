using TaskTracker.SharedKernel;

namespace TaskTracker.Domain.ValueObjects
{
    public sealed class Priority : ValueObject
    {
        public static readonly Priority Low = new("Low", 1);
        public static readonly Priority Medium = new("Medium", 2);
        public static readonly Priority High = new("High", 3);

        public string Name { get; }
        public int Level { get; }

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
                _ => throw new ArgumentException("Invalid priority level", nameof(level))
            };
        }

        public override string ToString() => Name;
    }

}
