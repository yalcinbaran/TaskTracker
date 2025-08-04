using MudBlazor;

namespace TaskTrackerUI.Services
{
    public class AppMessageState
    {
        public string? SnackbarMessage { get; set; }
        public Severity SnackbarSeverity { get; set; } = Severity.Normal;
    }

}
