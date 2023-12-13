using SampleApp.Api.Infrastructure.ErrorHandlers;

namespace SampleApp.Api.Extensions;

public static class ExceptionHandlerConfiguration
{
    public static void ConfigureExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<DefaultExceptionHandler>();
    }
}
