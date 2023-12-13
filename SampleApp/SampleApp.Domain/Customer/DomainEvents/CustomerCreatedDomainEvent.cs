using MediatR;

namespace SampleApp.Domain.Customer.DomainEvents;

public record CustomerCreatedDomainEvent(
    string Email,
    DateTime Created
) : INotification;
