using SampleApp.Domain.Customer.DomainEvents;

namespace SampleApp.Domain.UnitTest.Customer.DomainEvents;

[TestClass]
public class CustomerEmailChangedDomainEventTest
{
    [TestMethod]
    public void Test_CustomerEmailChangedDomainEvent_EqualValue_ReturnTrue()
    {
        var oldEmail = "test@mail.com";
        var newEmail = "test@mail.net";

        var one = new CustomerEmailChangedDomainEvent(oldEmail, newEmail);
        var two = one with { OldEmail = "test2@mail.com", NewEmail = "test2@mail.net" };

        Assert.IsTrue(one.OldEmail == oldEmail);
        Assert.IsTrue(one.NewEmail == newEmail);
        Assert.IsTrue(one != two);
    }
}
