using EntityCustomer = SampleApp.Domain.Customer.Entities.Customer;

namespace SampleApp.Domain.UnitTest.Customer.Entities;

[TestClass]
public class CustomerTest
{
    [TestMethod]
    public void Test_CustomerEntity_EqualValue_ReturnTrue()
    {
        var one = new EntityCustomer("test@mail.com");
        var two = one;

        Assert.IsTrue(EqualityComparer<EntityCustomer>.Default.Equals(one, two));
        Assert.IsTrue(object.Equals(one, two));
        Assert.IsTrue(one.Equals(two));
        Assert.IsTrue(one == two);
    }

    [TestMethod]
    public void Test_CustomerEntity_EqualValue_ReturnFalse()
    {
        var one = new EntityCustomer("test@mail.com");
        var two = new EntityCustomer("test@mail.com");

        Assert.IsFalse(EqualityComparer<EntityCustomer>.Default.Equals(one, two));
        Assert.IsFalse(object.Equals(one, two));
        Assert.IsFalse(one.Equals(two));
        Assert.IsFalse(one == two);
    }

    [TestMethod]
    public void Test_CustomerEntity_ChangeEmail_ReturnTrue()
    {
        var newEmail = "test@mail.net";

        var one = new EntityCustomer();
        one.SetEmail(newEmail);

        Assert.IsTrue(newEmail == one.Email);
        Assert.IsTrue(one.DomainEvents.Count == 1);
    }
}
