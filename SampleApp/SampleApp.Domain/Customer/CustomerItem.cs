using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Customer.ValueObjects;

namespace SampleApp.Domain.AggregatesModel.BankCustomer;

public class CustomerItem(CustomerInfo customer, Address address)
{
    public long Id { get; private set; }
    public CustomerInfo Customer { get; private set; } = customer;
    public Address Address { get; private set; } = address;
}
