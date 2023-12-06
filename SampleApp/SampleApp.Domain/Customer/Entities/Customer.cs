using SampleApp.Domain.Seeds;

namespace SampleApp.Domain.Customer.Entities;

public class Customer : Entity, IAggregateRoot
{
    public long Version => 1;
}
