using SampleApp.Domain.Customer.ValueObjects;

namespace SampleApp.Domain.UnitTest.Customer.ValueObjects;

[TestClass]
public class AddressTest
{
    [TestMethod]
    public void Test_AddressValueObject_EqualValue_ReturnTrue()
    {
        var one = new Address("1 Microsoft Way", "Redmond", "WA", "US", "98052");
        var two = new Address("1 Microsoft Way", "Redmond", "WA", "US", "98052");

        Assert.IsTrue(EqualityComparer<Address>.Default.Equals(one, two));
        Assert.IsTrue(object.Equals(one, two));
        Assert.IsTrue(one.Equals(two));
        Assert.IsTrue(one == two);
    }

    [TestMethod]
    public void Test_AddressValueObject_EqualValue_ReturnFalse()
    {
        var one = new Address("1 Microsoft Way", "Redmond", "WA", "US", "98052");
        var two = new Address("2 Microsoft Way", "Redmond", "WA", "US", "98052");

        Assert.IsFalse(EqualityComparer<Address>.Default.Equals(one, two));
        Assert.IsFalse(object.Equals(one, two));
        Assert.IsFalse(one.Equals(two));
        Assert.IsFalse(one == two);
    }
}