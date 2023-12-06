using EntityCustomer = SampleApp.Domain.Customer.Entities.Customer;
using SampleApp.Domain.Customer.ValueObjects;

namespace SampleApp.Domain.AggregatesModel.BankCustomer;

public class CustomerItem(EntityCustomer customer, Address address)
{
    public long Id { get; private set; }
    public EntityCustomer Customer { get; private set; } = customer;
    public Address Address { get; private set; } = address;
}
