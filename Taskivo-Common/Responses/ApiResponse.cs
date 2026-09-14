using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Taskivo_Common.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; } = true;

    public string? Message { get; set; }

    public T Data { get; set; } = default!;
}

public class ApiErrorResponse
{

[DefaultValue(false)]
    public bool Success { get; set; } = false;

    public string? Message { get; set; }

    public object? Data { get; set; }
}

public static class ApiResponse
{
    public static ApiResponse<T> Success<T>(
        T data,
        string? message = null)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static ApiErrorResponse Error(
        string message)
    {
        return new ApiErrorResponse
        {
            Success = false,
            Message = message,
            Data = null
        };
    }
}