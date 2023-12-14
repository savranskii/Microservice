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
            .WithTags("customer");
        // .AllowAnonymous();
        // .RequireAuthorization();
    }

    public static RouteGroupBuilder MapCustomerApi(this RouteGroupBuilder group)
    {
        var endpoint = new CustomerEndpoint();

        group.MapGet("/{id:long}", endpoint.GetCustomerAsync).WithName("GetCustomer").WithDescription("Get customer by ID");
        group.MapGet("/all", endpoint.GetCustomersAsync).WithName("GetCustomers").WithDescription("Retrieve all customers");
        group.MapPut("/search", endpoint.SearchCustomerAsync).WithName("SearchCustomer").WithDescription("Search custimers by filters");
        group.MapPost("/", endpoint.CreateCustomerAsync).WithName("CreateCustomer").WithDescription("Create new customer");
        group.MapPut("/", endpoint.UpdateCustomerAsync).WithName("UpdateCustomer").WithDescription("Update existing customer");
        group.MapDelete("/{id:long}", endpoint.DeleteCustomerAsync).WithName("DeleteCustomer").WithDescription("Delete customer by ID");

        return group;
    }
}
