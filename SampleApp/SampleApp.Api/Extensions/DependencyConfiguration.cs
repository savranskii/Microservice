using Microsoft.EntityFrameworkCore;
using SampleApp.Api.Application.Constants;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.Contexts;
using SampleApp.Infrastructure.ExternalServices;
using SampleApp.Infrastructure.Models.Options;
using SampleApp.Infrastructure.Repositories;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Extensions;

public static class DependencyConfiguration
{
    public static void ConfigureDependencies(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionOptions = new ConnectionOptions();
        configuration.GetSection(OptionsKey.Connection).Bind(connectionOptions);

        services.AddDbContext<CustomerContext>(opt => opt.UseInMemoryDatabase(connectionOptions.InMemory));
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IIntegrationEventSender, KafkaProducer>();
    }
}
