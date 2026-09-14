using Microsoft.AspNetCore.Mvc;
using Taskivo_AppServices;
using Taskivo_Commands;
using Taskivo_Commands.Task;
using Taskivo_Common.Extensions;
using Taskivo_Common.Responses;
using Taskivo_DTO;
using Taskivo_Infrastructure;
using Taskivo_Infrastructure.Repositories;
using Taskivo_Queries;
using Taskivo_Queries.Task;

namespace Taskivo_Controller.Extensions;
public static class StartUp
{
    public static void AddStartUpConfigurationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register services here
       services.AddInfrastructureServices(configuration);

services.AddTaskivoExceptionHandling();

services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(x => x.Value is not null && x.Value.Errors.Count > 0)
                .ToDictionary(
                    x => x.Key,
                    x => x.Value!.Errors.Select(error => error.ErrorMessage).ToArray());

            var response = ApiResponse.Error(
                "Validation failed. Please check the request payload."
            );
           

            return new BadRequestObjectResult(response);
        };
    });
services.AddScoped<ITaskRepository, TaskRepository>();
services.AddScoped<ITaskService, TaskService>();

// Command Handlers 
services.AddScoped<ICommandHandler<CreateTaskCommand, Guid>, CreateTaskCommandHandler>();
services.AddScoped<ICommandHandler<UpdateTaskCommand, TaskDto?>, UpdateTaskCommandHandler>();
services.AddScoped<ICommandHandler<DeleteTaskCommand, bool>, DeleteTaskCommandHandler>();

// Query Handlers
services.AddScoped<IQueryHandler<GetAllTaskQuery, List<TaskDto>>, GetAllTaskQueryHandler>();
services.AddScoped<IQueryHandler<GetTaskByIdQuery, TaskDto?>, GetTaskByIdQueryHandler>();

    }
}