using MediatR;
using SampleApp.Domain.Customer.DomainEvents;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api;

public class CustomerEmailChangedDomainEventHandler(ILogger<CustomerEmailChangedDomainEventHandler> logger)
    : INotificationHandler<CustomerEmailChangedDomainEvent>
{
    private readonly ILogger<CustomerEmailChangedDomainEventHandler> _logger = logger;

    public async Task Handle(CustomerEmailChangedDomainEvent customerCreatedDomainEvent, CancellationToken cancellationToken)
    {
        _logger.LogTrace(LogCategory.DomainEventHandler, "Event CustomerEmailChangedDomainEvent handled");
        await Task.CompletedTask;
    }
}
