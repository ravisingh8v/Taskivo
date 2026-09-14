using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Taskivo_Common.Exceptions;

namespace Taskivo_Common.Extensions;

public static class ExceptionHandlingExtensions
{
    public static IServiceCollection AddTaskivoExceptionHandling(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<TaskivoExceptionHandler>();

        return services;
    }
}
