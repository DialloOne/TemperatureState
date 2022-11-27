using SensorState.Exceptions;
using SensorState.Services;
using System.Net;
using System.Text.Json;

namespace SensorState.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode status;
        var stackTrace = String.Empty;
        string message;

        var exceptionType = exception.GetType();
        if (exceptionType == typeof(BadRequestException))
        {
            message = exception.Message;
            status = HttpStatusCode.BadRequest;
            stackTrace = exception.StackTrace;
        }
        else if (exceptionType == typeof(NotFoundException))
        {
            message = exception.Message;
            status = HttpStatusCode.NotFound;
            stackTrace = exception.StackTrace;
        }
        else if (exceptionType == typeof(NotImplementedException))
        {
            status = HttpStatusCode.NotImplemented;
            message = exception.Message;
            stackTrace = exception.StackTrace;
        }
        else if (exceptionType == typeof(UnauthorizedAccessException))
        {
            status = HttpStatusCode.Unauthorized;
            message = exception.Message;
            stackTrace = exception.StackTrace;
        }
        else if (exceptionType == typeof(KeyNotFoundException))
        {
            status = HttpStatusCode.Unauthorized;
            message = exception.Message;
            stackTrace = exception.StackTrace;
        }
        else
        {
            status = HttpStatusCode.InternalServerError;
            message = exception.Message;
            stackTrace = exception.StackTrace;
        }

        var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;
        return context.Response.WriteAsync(exceptionResult);
    }
}
