using SampleApp.Domain.Customer.Entities;

namespace SampleApp.Domain.UnitTest.Customer.Entities;

[TestClass]
public class CustomerTest
{
    [TestMethod]
    public void Test_CustomerInfoEntity_EqualValue_ReturnTrue()
    {
        var one = new CustomerInfo("test@mail.com");
        var two = one;

        Assert.IsTrue(EqualityComparer<CustomerInfo>.Default.Equals(one, two));
        Assert.IsTrue(object.Equals(one, two));
        Assert.IsTrue(one.Equals(two));
        Assert.IsTrue(one == two);
    }

    [TestMethod]
    public void Test_CustomerInfoEntity_EqualValue_ReturnFalse()
    {
        var one = new CustomerInfo("test@mail.com");
        var two = new CustomerInfo("test@mail.com");

        Assert.IsFalse(EqualityComparer<CustomerInfo>.Default.Equals(one, two));
        Assert.IsFalse(object.Equals(one, two));
        Assert.IsFalse(one.Equals(two));
        Assert.IsFalse(one == two);
    }

    [TestMethod]
    public void Test_CustomerInfoEntity_ChangeEmail_ReturnTrue()
    {
        var newEmail = "test@mail.net";

        var one = new CustomerInfo();
        one.SetEmail(newEmail);

        Assert.IsTrue(newEmail == one.Email);
        Assert.IsTrue(one.DomainEvents.Count == 1);
    }
}
