using SampleApp.Domain.Customer.ValueObjects;

namespace SampleApp.Domain.AggregatesModel.BankCustomer;

public class CustomerItem(Customer.Entities.Customer customer, Address address)
{
    public long Id { get; private set; }
    public Customer.Entities.Customer Customer { get; private set; } = customer;
    public Address Address { get; private set; } = address;
}
