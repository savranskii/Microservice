using EntityCustomer = SampleApp.Domain.Customer.Entities.Customer;
using SampleApp.Domain.Customer.ValueObjects;

namespace SampleApp.Domain.AggregatesModel.BankCustomer;

public class CustomerItem
{
    public long Id { get; private set; }
    public EntityCustomer Customer { get; private set; }
    public Address Address { get; private set; }

    public CustomerItem(EntityCustomer customer, Address address)
    {
        Customer = customer;
        Address = address;
    }
}
