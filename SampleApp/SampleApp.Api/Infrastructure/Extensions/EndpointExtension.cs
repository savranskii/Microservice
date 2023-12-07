﻿using SampleApp.Api.Infrastructure.Endpoints;

namespace SampleApp.Api.Infrastructure.Extensions;

public static class EndpointExtension
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        app.MapGroup("/api/customer")
            .MapCustomerApi();
        // .WithTags("Public")
        // .AllowAnonymous();
        // .RequireAuthorization();
    }

    public static RouteGroupBuilder MapCustomerApi(this RouteGroupBuilder group)
    {
        var endpoint = new CustomerEndpoint();

        group.MapGet("/{id:long}", endpoint.GetCustomerAsync).WithName("GetCustomer").WithOpenApi();
        group.MapGet("/all", endpoint.GetCustomersAsync).WithName("GetCustomers").WithOpenApi();
        group.MapPut("/search", endpoint.SearchCustomerAsync).WithName("SearchCustomer").WithOpenApi();
        group.MapPost("/", endpoint.CreateCustomerAsync).WithName("CreateCustomer").WithOpenApi();
        group.MapPut("/", endpoint.UpdateCustomerAsync).WithName("UpdateCustomer").WithOpenApi();
        group.MapDelete("/{id:long}", endpoint.DeleteCustomerAsync).WithName("DeleteCustomer").WithOpenApi();

        return group;
    }
}
