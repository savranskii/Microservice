using SampleApp.Domain.Seeds;

namespace SampleApp.Domain.Customer.Entities;

public class Customer : Entity, IAggregateRoot
{
    public long Version => 1;
    public string Email { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}
