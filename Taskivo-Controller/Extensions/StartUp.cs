using Taskivo_Commands.Auth;
using Taskivo_Queries.Auth;
using Taskivo_Infrastructure.Models;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi;

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
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience= configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? string.Empty))
    };
});

services.AddAuthorization();

services.AddEndpointsApiExplorer();
services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Taskivo API v1",
        Version = "v1",
        Description = "Taskivo Task Management API"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token."
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", document),
            new List<string>()
        }
    });
});




    
services.AddScoped<ITaskRepository, TaskRepository>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<ITaskService, TaskService>();
services.AddScoped<IAuthService, AuthService>();

services.AddScoped<ICommandHandler<CreateUserCommand, UserEntity>, CreateUserCommandHandler>();
services.AddScoped<IQueryHandler<UserExistsQuery, bool>, UserExistsQueryHandler>();
services.AddScoped<IQueryHandler<GetUserByUsernameQuery, UserEntity?>, GetUserByUsernameQueryHandler>();

// Command Handlers 
services.AddScoped<ICommandHandler<CreateTaskCommand, Guid>, CreateTaskCommandHandler>();
services.AddScoped<ICommandHandler<UpdateTaskCommand, TaskDto?>, UpdateTaskCommandHandler>();
services.AddScoped<ICommandHandler<UpdateTaskStatusCommand, TaskDto?>, UpdateTaskStatusCommandHandler>();
services.AddScoped<ICommandHandler<DeleteTaskCommand, bool>, DeleteTaskCommandHandler>();

// Query Handlers
services.AddScoped<IQueryHandler<GetAllTaskQuery, List<TaskDto>>, GetAllTaskQueryHandler>();
services.AddScoped<IQueryHandler<GetTaskByIdQuery, TaskDto?>, GetTaskByIdQueryHandler>();

    }
}