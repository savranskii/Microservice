using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using SampleApp.Api.Application.Constants;
using SampleApp.Infrastructure.Models.Options;

namespace SampleApp.Api.Infrastructure.Configurations;

public static class RateLimitConfiguration
{
    public static void ConfigureRateLimit(this IServiceCollection services, ConfigurationManager configuration)
    {
        var rateLimitOptions = new RateLimitOptions();
        configuration.GetSection(OptionsKey.RateLimit).Bind(rateLimitOptions);

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddFixedWindowLimiter(policyName: RateLimitPolicy.FixedPolicy, options =>
            {
                options.PermitLimit = rateLimitOptions.PermitLimit;
                options.Window = TimeSpan.FromSeconds(rateLimitOptions.Window);
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = rateLimitOptions.QueueLimit;
            });
        });
    }
}
