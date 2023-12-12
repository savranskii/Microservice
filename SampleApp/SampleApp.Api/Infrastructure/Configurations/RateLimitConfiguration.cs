using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using SampleApp.Api.Infrastructure.Options;

namespace SampleApp.Api.Infrastructure.Configurations;

public static class RateLimitConfiguration
{
    public static void ConfigureRateLimit(this IServiceCollection services, RateLimitOptions rateLimitOptions)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddFixedWindowLimiter(policyName: RateLimitOptions.FixedPolicy, options =>
            {
                options.PermitLimit = rateLimitOptions.PermitLimit;
                options.Window = TimeSpan.FromSeconds(rateLimitOptions.Window);
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = rateLimitOptions.QueueLimit;
            });
        });
    }
}
