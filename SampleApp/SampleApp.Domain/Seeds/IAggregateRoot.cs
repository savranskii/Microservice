using MediatR;

namespace SampleApp.Domain.Seeds;

public interface IAggregateRoot
{
    long Version { get; }
    void AddDomainEvent(INotification domainEvent);
    void ClearDomainEvents();
}
