using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SampleApp.Infrastructure.Services.HealthCheck;

public class DbHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var isHealthy = true;

        // ...

        if (isHealthy)
        {
            return Task.FromResult(HealthCheckResult.Healthy("A healthy result."));
        }

        return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "An unhealthy result."));
    }
}
