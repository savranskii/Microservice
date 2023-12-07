using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Api.Application.Models;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.DomainEvents;

namespace SampleApp.Api.Infrastructure.Endpoints;

public static class CustomerEndpoint
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        var customerApi = app.MapGroup("/api/customer");

        customerApi.MapGet("/{id:long}", GetCustomerAsync).WithName("GetCustomer").WithOpenApi();
        customerApi.MapGet("/all", GetCustomersAsync).WithName("GetCustomers").WithOpenApi();
        customerApi.MapPut("/search", SearchCustomerAsync).WithName("SearchCustomer").WithOpenApi();
        customerApi.MapPost("/", CreateCustomerAsync).WithName("CreateCustomer").WithOpenApi();
        customerApi.MapPut("/", UpdateCustomerAsync).WithName("UpdateCustomer").WithOpenApi();
        customerApi.MapDelete("/{id:long}", DeleteCustomerAsync).WithName("DeleteCustomer").WithOpenApi();
    }

    static async Task<IResult> GetCustomerAsync(long id, IMediator mediator, ILogger<Program> logger)
    {
        logger.LogInformation("Execute get by id customer");

        var customer = await mediator.Send(new GetCustomerQuery(id));

        return customer is null ? TypedResults.NotFound() : TypedResults.Ok(customer);
    }

    static async Task<IResult> GetCustomersAsync(IMediator mediator, ILogger<Program> logger)
    {
        logger.LogInformation("Execute get all customer");

        var customers = await mediator.Send(new GetCustomersQuery());

        return TypedResults.Ok(customers);
    }

    static async Task<IResult> SearchCustomerAsync(SearchCustomerRequest data, IMediator mediator, ILogger<Program> logger)
    {
        logger.LogInformation("Execute search customer");

        var customer = await mediator.Send(new SearchCustomerQuery(data));

        return customer is null ? TypedResults.NotFound() : TypedResults.Ok(customer);
    }

    static async Task<IResult> CreateCustomerAsync(CreateCustomerRequest data, IMediator mediator, ILogger<Program> logger)
    {
        logger.LogInformation("Execute create customer");

        var customerId = await mediator.Send(new CreateCustomerCommand(data.Email));

        await mediator.Publish(new CustomerCreatedDomainEvent(1, "debit", "123", "John Doe", DateTime.UtcNow));

        return TypedResults.Ok(customerId);
    }

    static async Task<IResult> UpdateCustomerAsync(UpdateCustomerRequest data, IMediator mediator, ILogger<Program> logger)
    {
        logger.LogInformation("Execute update customer");

        await mediator.Send(new UpdateCustomerCommand(data.Id, data.Item));

        return Results.NoContent();
    }

    static async Task<IResult> DeleteCustomerAsync(long id, IMediator mediator, ILogger<Program> logger)
    {
        logger.LogInformation("Execute delete customer");

        await mediator.Send(new DeleteCustomerCommand(id));

        return Results.NoContent();
    }
}
