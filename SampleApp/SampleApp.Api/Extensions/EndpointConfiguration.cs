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
        group.MapGet("/{id:long}", CustomerEndpoint.GetAsync).WithDescription("Get customer by ID");
        group.MapGet("/all", CustomerEndpoint.GetAllAsync).WithDescription("Retrieve all customers");
        group.MapPut("/search", CustomerEndpoint.SearchAsync).WithDescription("Search customers by filters");
        group.MapPost("/", CustomerEndpoint.CreateAsync).WithDescription("Create new customer");
        group.MapPut("/{id:long}", CustomerEndpoint.UpdateAsync).WithDescription("Update existing customer");
        group.MapDelete("/{id:long}", CustomerEndpoint.DeleteAsync).WithDescription("Delete customer by ID");

        return group;
    }
}
