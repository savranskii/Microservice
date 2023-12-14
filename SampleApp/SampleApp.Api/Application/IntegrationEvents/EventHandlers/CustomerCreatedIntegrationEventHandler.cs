﻿using MediatR;
using SampleApp.Api.Application.Constants;
using SampleApp.Api.Application.IntegrationEvents.Events;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.ExternalServices;

namespace SampleApp.Api.IntegrationEvents.EventHandlers;

public class CustomerCreatedIntegrationEventHandler : INotificationHandler<CustomerCreatedIntegrationEvent>
{
    private readonly IIntegrationEventSender _sender;
    private readonly ILogger<CustomerCreatedIntegrationEventHandler> _logger;

    public CustomerCreatedIntegrationEventHandler(
        ILogger<CustomerCreatedIntegrationEventHandler> logger,
        [FromKeyedServices(ServiceKey.Kafka)] IIntegrationEventSender sender)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    public async Task Handle(CustomerCreatedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("---- Sending integration event: {CommandName} - {IdProperty}", nameof(@event), @event.CustomerId);
        await _sender.SendMessageAsync(MessageTopic.CustomerCreated, @event);
        // await Task.CompletedTask;
    }
}
