using SampleApp.Domain.Customer.DomainEvents;
using SampleApp.Domain.Seeds;

namespace SampleApp.Domain.Customer.Entities;

public class Customer : Entity, IAggregateRoot
{
    public long Version => 1;
    public string Email { get; private set; } = string.Empty;
    public DateTime Created { get; private set; } = DateTime.UtcNow;

    public Customer()
    {
    }

    public Customer(string email)
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
