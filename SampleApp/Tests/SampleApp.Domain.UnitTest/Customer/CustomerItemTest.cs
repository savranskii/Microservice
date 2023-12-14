using EntityCustomer = SampleApp.Domain.Customer.Entities.Customer;
using SampleApp.Domain.AggregatesModel.BankCustomer;
using SampleApp.Domain.Customer.ValueObjects;

namespace SampleApp.Domain.UnitTest.Customer;

[TestClass]
public class CustomerItemTest
{
    [TestMethod]
    public void Test_CustomerItem_ValidValue()
    {
        var entityCustomer = new EntityCustomer();
        var address = new Address("1 Microsoft Way", "Redmond", "WA", "US", "98052");
        var item = new CustomerItem(entityCustomer, address);

        Assert.IsNotNull(item.Customer);
        Assert.IsNotNull(item.Address);
    }
}
