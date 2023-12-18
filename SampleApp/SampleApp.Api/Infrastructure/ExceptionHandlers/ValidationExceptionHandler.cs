using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api.Infrastructure.ExceptionHandlers;

public class ValidationExceptionHandler(ILogger<DefaultExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<DefaultExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(LogCategory.ExceptionHandler, exception, "A validation error occurred");

        if (exception is ValidationException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = exception.GetType().Name,
                Title = "A validation error occurred",
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            }, cancellationToken: cancellationToken);
            return true;
        }
        return false;
    }
}
