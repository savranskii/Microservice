using SampleApp.Domain.Customer.DomainEvents;

namespace SampleApp.Domain.UnitTest.Customer.DomainEvents;

[TestClass]
public class CustomerCreatedDomainEventTest
{
    [TestMethod]
    public void Test_CustomerCreatedDomainEvent_EqualValue_ReturnTrue()
    {
        var email = "test@mail.com";
        var created = DateTime.UtcNow;

        var one = new CustomerCreatedDomainEvent(email, created);
        var two = one with { Email = "test@mail.net" };

        Assert.IsTrue(one.Email == email);
        Assert.IsTrue(one.Created == created);
        Assert.IsTrue(one != two);
    }
}
