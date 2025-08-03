using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Commands.Handlers;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries.Handlers;
using TaskTracker.Application.Tasks.Commands.Handlers;
using TaskTracker.Application.Tasks.Queries.Handlers;

namespace TaskTracker.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<BulkDeleteTasksCommandHandler>(); // Yapıldı
            services.AddScoped<ChangePriorityCommandHandler>(); // Yapıldı
            services.AddScoped<ChangeStateCommandHandler>(); // Yapıldı
            services.AddScoped<CreateTaskCommandHandler>(); // Yapıldı
            services.AddScoped<DeleteTaskCommandHandler>(); // Yapıldı
            services.AddScoped<UpdateTaskCommandHandler>(); // Yapıldı
            services.AddScoped<GetAllTasksQueryHandler>(); // Yapıldı
            services.AddScoped<GetOverdueTasksQueryHandler>();
            services.AddScoped<GetTaskByIdQueryHandler>(); // Yapıldı
            services.AddScoped<GetTasksByPriorityQueryHandler>(); // Yapıldı
            services.AddScoped<GetTasksByStateQueryHandler>(); // Yapıldı
            services.AddScoped<SearchTasksQueryHandler>();
            return services;
        }
    }
}
