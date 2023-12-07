using MediatR;
using SampleApp.Domain.Customer.DomainEvents;

namespace SampleApp.Api.Application.Handlers;

public class CreateCustomerEventHandler(ILogger<CreateCustomerEventHandler> logger) : INotificationHandler<CustomerCreatedDomainEvent>
{
    private readonly ILogger<CreateCustomerEventHandler> _logger = logger;

    public async Task Handle(CustomerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event 'CustomerCreatedDomainEvent' handled.");

        await Task.CompletedTask;
    }
}
