using SampleApp.Api.Application.Constants;
using SampleApp.Api.Infrastructure.Endpoints;

namespace SampleApp.Api.Extensions;

public static class EndpointConfiguration
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        app.MapGroup("/api/customer")
            .MapCustomerApi()
            .RequireRateLimiting(RateLimitPolicy.FixedPolicy)
            .WithOpenApi()
            .WithTags("Customer");
        // .AllowAnonymous();
        // .RequireAuthorization();
    }

    public static RouteGroupBuilder MapCustomerApi(this RouteGroupBuilder group)
    {
        var endpoint = new CustomerEndpoint();

        group.MapGet("/{id:long}", endpoint.GetCustomerAsync).WithDescription("Get customer by ID");
        group.MapGet("/all", endpoint.GetCustomersAsync).WithDescription("Retrieve all customers");
        group.MapPut("/search", endpoint.SearchCustomerAsync).WithDescription("Search customers by filters");
        group.MapPost("/", endpoint.CreateCustomerAsync).WithDescription("Create new customer");
        group.MapPut("/", endpoint.UpdateCustomerAsync).WithDescription("Update existing customer");
        group.MapDelete("/{id:long}", endpoint.DeleteCustomerAsync).WithDescription("Delete customer by ID");

        return group;
    }
}
