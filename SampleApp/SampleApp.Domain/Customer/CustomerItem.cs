using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Customer.ValueObjects;

namespace SampleApp.Domain.AggregatesModel.BankCustomer;

public class CustomerItem
{
    public long Id { get; private set; }
    public CustomerInfo Customer { get; private set; }
    public Address Address { get; private set; }

    public CustomerItem(CustomerInfo customer, Address address)
    {
        Customer = customer;
        Address = address;
    }
}
