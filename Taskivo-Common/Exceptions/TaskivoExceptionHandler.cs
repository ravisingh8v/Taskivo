using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Taskivo_Common.Exceptions;
using Taskivo_Common.Responses;

namespace Taskivo_Common.Exceptions;

public class TaskivoExceptionHandler : IExceptionHandler
{
    private readonly ILogger<TaskivoExceptionHandler> _logger;

    public TaskivoExceptionHandler(ILogger<TaskivoExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is OperationCanceledException)
        {
            return false;
        }

        _logger.LogError(exception, "Unhandled exception occurred.");

        var (statusCode, message) = exception switch
        {
            BusinessException businessException => (
                businessException.StatusCode,
                businessException.Message),
            _ => (
                StatusCodes.Status500InternalServerError,
                "Something went wrong. Please try again later.")
        };

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        var response = new ApiErrorResponse
        {
            Message = message,
            Data = null
        };

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
