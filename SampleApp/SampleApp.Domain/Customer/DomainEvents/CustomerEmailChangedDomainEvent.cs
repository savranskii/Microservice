using MediatR;

namespace SampleApp.Domain.Customer.DomainEvents;

public record CustomerEmailChangedDomainEvent(
    string OldEmail,
    string NewEmail
) : INotification;
