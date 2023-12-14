using SampleApp.Domain;

namespace SampleApp.Api.Application.IntegrationEvents.Events;

public record CustomerCreatedIntegrationEvent(string CustomerId) : IIntegrationEvent;
