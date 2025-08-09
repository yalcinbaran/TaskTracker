namespace TaskTracker.Shared.Common
{
    public class UserSession
    {
        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public bool IsAuthenticated => UserId != Guid.Empty;
    }

}
