namespace SampleApp.Domain.Seeds;

public interface IAggregateRoot
{
    long Version { get; }
    void AddDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
