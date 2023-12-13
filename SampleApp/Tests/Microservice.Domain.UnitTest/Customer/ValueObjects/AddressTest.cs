namespace Microservice.Domain.UnitTest.Customer.ValueObjects;

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
}