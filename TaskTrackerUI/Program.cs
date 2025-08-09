using MudBlazor;
using MudBlazor.Services;
using TaskTracker.Shared.Interfaces;
using TaskTracker.Shared.Services;
using TaskTrackerUI.Components;
using TaskTrackerUI.Interfaces;
using TaskTrackerUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient("ApiClient", client =>
{
    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];
    if (string.IsNullOrEmpty(baseUrl))
    {
        throw new Exception("API BaseUrl deðeri 'appsettings.json' içinde tanýmlý deðil!");
    }

    client.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddScoped<IUserSessionService, UserSessionService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskApiService, TaskApiService>();
builder.Services.AddScoped<AppMessageState>();
builder.Services.AddScoped<IStateAndPriorityService, StateAndPriorityService>();
builder.Services.AddScoped<IActiveTaskService, ActiveTasksService>();
builder.Services.AddScoped<ICompletedTasksService, CompletedTasksService>();
builder.Services.AddScoped<IOverdueTasksService, OverdueTasksService>();
builder.Services.AddScoped<ICanceledTasksService, CanceledTasksService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
