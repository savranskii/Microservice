using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Api.Application.Commands;
using SampleApp.Api.Application.Models;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.DomainEvents;
using SampleApp.Domain.Customer.Entities;

namespace SampleApp.Api.Infrastructure.Endpoints;

public static class CustomerEndpoint
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        app.MapGroup("/api/customer")
            .MapCustomerApi()
            .WithTags("Public");
        // .AllowAnonymous();
        // .RequireAuthorization();
    }

    public static RouteGroupBuilder MapCustomerApi(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:long}", GetCustomerAsync)
            .Produces<Customer>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetCustomer")
            .WithOpenApi();

        group.MapGet("/all", GetCustomersAsync)
            .Produces<IEnumerable<Customer>>(StatusCodes.Status200OK)
            .WithName("GetCustomers")
            .WithOpenApi();

        group.MapPut("/search", SearchCustomerAsync)
            .Produces<Customer>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("SearchCustomer")
            .WithOpenApi();

        group.MapPost("/", CreateCustomerAsync)
            .Produces<long>(StatusCodes.Status200OK)
            .WithName("CreateCustomer")
            .WithOpenApi();

        group.MapPut("/", UpdateCustomerAsync)
            .Produces(StatusCodes.Status204NoContent)
            .WithName("UpdateCustomer")
            .WithOpenApi();

        group.MapDelete("/{id:long}", DeleteCustomerAsync)
            .Produces(StatusCodes.Status204NoContent)
            .WithName("DeleteCustomer")
            .WithOpenApi();

        return group;
    }

    static async Task<IResult> GetCustomerAsync(
        [FromRoute] long id,
        [FromServices] IMediator mediator,
        [FromServices] ILogger<Program> logger)
    {
        logger.LogInformation("Execute get by id customer");

        var customer = await mediator.Send(new GetCustomerQuery(id));

        return customer is null ? TypedResults.NotFound() : TypedResults.Ok(customer);
    }

    static async Task<IResult> GetCustomersAsync(
        [FromServices] IMediator mediator,
        [FromServices] ILogger<Program> logger)
    {
        logger.LogInformation("Execute get all customer");

        var customers = await mediator.Send(new GetCustomersQuery());

        return TypedResults.Ok(customers);
    }

    static async Task<IResult> SearchCustomerAsync(
        [FromBody] SearchCustomerRequest data,
        [FromServices] IMediator mediator,
        [FromServices] ILogger<Program> logger)
    {
        logger.LogInformation("Execute search customer");

        var customer = await mediator.Send(new SearchCustomerQuery(data));

        return customer is null ? TypedResults.NotFound() : TypedResults.Ok(customer);
    }

    static async Task<IResult> CreateCustomerAsync(
        [FromBody] CreateCustomerRequest data,
        [FromServices] IMediator mediator,
        [FromServices] ILogger<Program> logger)
    {
        logger.LogInformation("Execute create customer");

        var customerId = await mediator.Send(new CreateCustomerCommand(data.Email));

        await mediator.Publish(new CustomerCreatedDomainEvent(1, "debit", "123", "John Doe", DateTime.UtcNow));

        return TypedResults.Ok(customerId);
    }

    static async Task<IResult> UpdateCustomerAsync(
        [FromBody] UpdateCustomerRequest data,
        [FromServices] IMediator mediator,
        [FromServices] ILogger<Program> logger)
    {
        logger.LogInformation("Execute update customer");

        await mediator.Send(new UpdateCustomerCommand(data.Id, data.Item));

        return Results.NoContent();
    }

    static async Task<IResult> DeleteCustomerAsync(
        [FromRoute] long id,
        [FromServices] IMediator mediator,
        [FromServices] ILogger<Program> logger)
    {
        logger.LogInformation("Execute delete customer");

        await mediator.Send(new DeleteCustomerCommand(id));

        return Results.NoContent();
    }
}
