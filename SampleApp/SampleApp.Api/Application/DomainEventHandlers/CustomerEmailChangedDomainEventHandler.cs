using MediatR;
using SampleApp.Domain.Customer.DomainEvents;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api;

public class CustomerEmailChangedDomainEventHandler : INotificationHandler<CustomerEmailChangedDomainEvent>
{
    private readonly ILogger<CustomerEmailChangedDomainEventHandler> _logger;

    public CustomerEmailChangedDomainEventHandler(ILogger<CustomerEmailChangedDomainEventHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(CustomerEmailChangedDomainEvent customerCreatedDomainEvent, CancellationToken cancellationToken)
    {
        _logger.LogTrace(LogCategory.DomainEventHandler, "---- Event CustomerEmailChangedDomainEvent handled");
        await Task.CompletedTask;
    }
}
