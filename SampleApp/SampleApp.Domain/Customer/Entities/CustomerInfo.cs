using SampleApp.Domain.Customer.DomainEvents;
using SampleApp.Domain.Seeds;

namespace SampleApp.Domain.Customer.Entities;

public class CustomerInfo : Entity, IAggregateRoot
{
    public long Version => 1;
    public string Email { get; private set; } = string.Empty;
    public DateTime Created { get; private set; } = DateTime.UtcNow;

    public CustomerInfo()
    {
    }

    public CustomerInfo(string email)
    {
        Email = email;
        Created = DateTime.UtcNow;

        AddDomainEvent(new CustomerCreatedDomainEvent(Email, Created));
    }

    public void SetEmail(string email)
    {
        AddDomainEvent(new CustomerEmailChangedDomainEvent(Email, email));

        Email = email;
    }
}
