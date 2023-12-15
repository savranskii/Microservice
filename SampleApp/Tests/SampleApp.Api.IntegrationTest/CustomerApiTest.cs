using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Assert = Xunit.Assert;

namespace SampleApp.Api.IntegrationTest;

public class CustomerApiTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CustomerApiTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CreateCustomer_ReturnsOkOfCustomerResult()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsync("/api/customer", new StringContent(JsonSerializer.Serialize(new { email = "test@mail.com" })));

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        var result = await response.Content.ReadAsStringAsync();
        var id = Convert.ToInt64(result);
        
        Assert.IsType<Results<Ok<long>, NotFound>>(id);
        Assert.Equal(1, id);
    }
}
