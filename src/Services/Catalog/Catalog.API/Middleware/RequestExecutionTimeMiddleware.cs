using System.Diagnostics;

namespace Catalog.API.Middleware;

public class RequestExecutionTimeMiddleware
{
    private readonly RequestDelegate _next;

    public RequestExecutionTimeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext,ILogger<RequestExecutionTimeMiddleware> logger)
    {
        logger.LogInformation($"Request send in {DateTime.Now}");
        var startTime = Stopwatch.GetTimestamp();
        
        await _next(httpContext);
        
        logger.LogInformation(1,$"Request with path {httpContext.Request.Path} was executed for {Stopwatch.GetElapsedTime(startTime).TotalMilliseconds} ms");
    }
}

public static class RequestExecutionTimeMiddlewareExtension
{
    public static IApplicationBuilder UseRequestExecutionTimeMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestExecutionTimeMiddleware>();
    }
}