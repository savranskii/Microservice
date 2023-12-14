using MediatR;
using SampleApp.Domain.Customer.DomainEvents;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api;

public class CustomerCreatedDomainEventHandler : INotificationHandler<CustomerCreatedDomainEvent>
{
    private readonly ILogger<CustomerCreatedDomainEventHandler> _logger;

    public CustomerCreatedDomainEventHandler(ILogger<CustomerCreatedDomainEventHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(CustomerCreatedDomainEvent customerCreatedDomainEvent, CancellationToken cancellationToken)
    {
        _logger.LogTrace(LogCategory.DomainEventHandler, "---- Event CustomerCreatedDomainEvent handled");
        await Task.CompletedTask;
    }
}
