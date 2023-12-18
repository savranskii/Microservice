using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api.Infrastructure.ExceptionHandlers;

public class DefaultExceptionHandler(ILogger<DefaultExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<DefaultExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(LogCategory.ExceptionHandler, exception, "An unexpected error occurred");

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Type = exception.GetType().Name,
            Title = "An unexpected error occurred",
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        }, cancellationToken: cancellationToken);

        return true;
    }
}
