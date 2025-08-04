using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Commands.Handlers;
using TaskTracker.Application.CommandsQueriesHandlers.Tasks.Queries.Handlers;
using TaskTracker.Application.CommandsQueriesHandlers.User.Commands.Handlers;
using TaskTracker.Application.CommandsQueriesHandlers.User.Queries.Handlers;
using TaskTracker.Application.Tasks.Commands.Handlers;
using TaskTracker.Application.Tasks.Queries.Handlers;

namespace TaskTracker.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<BulkDeleteTasksCommandHandler>(); 
            services.AddScoped<ChangePriorityCommandHandler>(); 
            services.AddScoped<ChangeStateCommandHandler>(); 
            services.AddScoped<CreateTaskCommandHandler>(); 
            services.AddScoped<DeleteTaskCommandHandler>();
            services.AddScoped<UpdateTaskCommandHandler>();
            services.AddScoped<GetAllTasksQueryHandler>();
            services.AddScoped<GetOverdueTasksQueryHandler>();
            services.AddScoped<GetTaskByIdQueryHandler>(); 
            services.AddScoped<GetTasksByPriorityQueryHandler>(); 
            services.AddScoped<GetTasksByStateQueryHandler>(); 
            services.AddScoped<SearchTasksQueryHandler>();
            services.AddScoped<CreateUserCommandHandler>();
            services.AddScoped<LoginCommandHandler>();
            services.AddScoped<GetUserByIdQueryHandler>();
            services.AddScoped<DeleteUserCommandHandler>();
            services.AddScoped<UpdateUserCommandHandler>();
            services.AddScoped<GetPriorityQueryHandler>();
            services.AddScoped<GetStatesQueryHandler>();
            return services;
        }
    }
}
