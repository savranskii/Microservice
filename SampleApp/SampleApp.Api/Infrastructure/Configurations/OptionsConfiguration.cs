using SampleApp.Api.Application.Constants;
using SampleApp.Infrastructure.Models.Options;

namespace SampleApp.Api.Infrastructure.Configurations;

public static class OptionsConfiguration
{
    public static void ConfigureOptions(this IServiceCollection services)
    {
        services.AddOptions<KafkaOptions>().BindConfiguration(OptionsKey.Kafka).ValidateDataAnnotations().ValidateOnStart();
        services.AddOptions<RateLimitOptions>().BindConfiguration(OptionsKey.RateLimit).ValidateDataAnnotations().ValidateOnStart();
        services.AddOptions<ConnectionOptions>().BindConfiguration(OptionsKey.Connection).ValidateDataAnnotations().ValidateOnStart();
    }
}
