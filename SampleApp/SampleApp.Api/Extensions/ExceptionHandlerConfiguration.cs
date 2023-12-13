using SampleApp.Api.Infrastructure.ExceptionHandlers;

namespace SampleApp.Api.Extensions;

public static class ExceptionHandlerConfiguration
{
    public static void ConfigureExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<DefaultExceptionHandler>();
    }
}
