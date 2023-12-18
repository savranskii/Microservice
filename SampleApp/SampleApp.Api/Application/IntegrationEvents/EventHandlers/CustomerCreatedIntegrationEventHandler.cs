using MediatR;
using SampleApp.Api.Application.IntegrationEvents.Events;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.ExternalServices;

namespace SampleApp.Api.IntegrationEvents.EventHandlers;

public class CustomerCreatedIntegrationEventHandler(
    ILogger<CustomerCreatedIntegrationEventHandler> logger,
    IIntegrationEventSender sender)
    : INotificationHandler<CustomerCreatedIntegrationEvent>
{
    private readonly IIntegrationEventSender _sender = sender;
    private readonly ILogger<CustomerCreatedIntegrationEventHandler> _logger = logger;

    public async Task Handle(CustomerCreatedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogCategory.IntegrationEventHandler, "Sending integration event: {CommandName} - {IdProperty}", nameof(@event), @event.CustomerId);
        // await _sender.SendMessageAsync(MessageTopic.CustomerCreated, @event);
        await Task.CompletedTask;
    }
}
