using SampleApp.Domain.Seeds;

namespace SampleApp.Domain.Customer.ValueObjects;

public record Address(string Street, string City, string State, string Country, string ZipCode) : ValueObject;
