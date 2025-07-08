using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace DragonBallBattles.API.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var data = new
        {
            Instance = httpContext.Request.Path,
            Title = exception.Message,
            Status = 500
        };
        _logger.LogError($"{DateTime.Now:yyyy-MM-dd} => {exception.Message} || {exception.StackTrace}");
        httpContext.Response.StatusCode = 500;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new { success = false, error = data }), cancellationToken);
        return true;
    }
}
