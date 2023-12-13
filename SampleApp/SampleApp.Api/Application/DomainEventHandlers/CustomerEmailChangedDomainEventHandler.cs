using MediatR;
using SampleApp.Domain.Customer.DomainEvents;

namespace SampleApp.Api;

public class CustomerEmailChangedDomainEventHandler : INotificationHandler<CustomerEmailChangedDomainEvent>
{
    private readonly ILogger<CustomerEmailChangedDomainEventHandler> _logger;

    public CustomerEmailChangedDomainEventHandler(ILogger<CustomerEmailChangedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(CustomerEmailChangedDomainEvent customerCreatedDomainEvent, CancellationToken cancellationToken)
    {
        _logger.LogTrace("---- Event CustomerEmailChangedDomainEvent handled");
        await Task.CompletedTask;
    }
}
