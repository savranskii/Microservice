using MediatR;
using SampleApp.Domain.Customer.DomainEvents;

namespace SampleApp.Api;

public class CustomerCreatedDomainEventHandler : INotificationHandler<CustomerCreatedDomainEvent>
{
    private readonly ILogger<CustomerCreatedDomainEventHandler> _logger;

    public CustomerCreatedDomainEventHandler(ILogger<CustomerCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(CustomerCreatedDomainEvent customerCreatedDomainEvent, CancellationToken cancellationToken)
    {
        _logger.LogTrace("---- Event CustomerCreatedDomainEvent handled");
        await Task.CompletedTask;
    }
}
