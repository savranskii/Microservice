using MediatR;
using SampleApp.Domain.Customer.DomainEvents;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api;

public class CustomerCreatedDomainEventHandler(ILogger<CustomerCreatedDomainEventHandler> logger)
    : INotificationHandler<CustomerCreatedDomainEvent>
{
    private readonly ILogger<CustomerCreatedDomainEventHandler> _logger = logger;

    public async Task Handle(CustomerCreatedDomainEvent customerCreatedDomainEvent, CancellationToken cancellationToken)
    {
        _logger.LogTrace(LogCategory.DomainEventHandler, "Event CustomerCreatedDomainEvent handled");
        await Task.CompletedTask;
    }
}
