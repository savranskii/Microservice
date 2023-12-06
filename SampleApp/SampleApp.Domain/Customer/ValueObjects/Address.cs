using SampleApp.Domain.Seeds;

namespace SampleApp.Domain.Customer.ValueObjects;

public class Address(string street, string city, string state, string country, string zipCode) : ValueObject
{
    public string Street { get; private set; } = street;
    public string City { get; private set; } = city;
    public string State { get; private set; } = state;
    public string Country { get; private set; } = country;
    public string ZipCode { get; private set; } = zipCode;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return Country;
        yield return ZipCode;
    }
}
