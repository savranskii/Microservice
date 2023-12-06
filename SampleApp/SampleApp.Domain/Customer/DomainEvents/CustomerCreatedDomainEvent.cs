using MediatR;

namespace SampleApp.Domain.Customer.DomainEvents;

public class CustomerCreatedDomainEvent(int cardTypeId, string cardNumber, string cardSecurityNumber, string cardHolderName, DateTime cardExpiration) : INotification
{
    public int CardTypeId { get; init; } = cardTypeId;
    public string CardNumber { get; init; } = cardNumber;
    public string CardSecurityNumber { get; init; } = cardSecurityNumber;
    public string CardHolderName { get; init; } = cardHolderName;
    public DateTime CardExpiration { get; init; } = cardExpiration;
}
