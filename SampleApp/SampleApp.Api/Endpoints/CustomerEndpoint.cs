using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Api.Application.Models;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.DomainEvents;

namespace SampleApp.Api;

public static class CustomerEndpoint
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        app.MapGet("/api/customer/{email}", async (string email, IMediator mediator, ILogger<Program> logger) =>
        {
            logger.LogInformation("Execute get customer");

            var customer = await mediator.Send(new GetCustomerByEmailQuery(email));

            return customer is null
                ? Results.NotFound()
                : Results.Ok(customer);
        })
        .WithName("GetCustomer")
        .WithOpenApi();

        app.MapPost("/api/customer", async (CreateCustomerRequest data, IMediator mediator, ILogger<Program> logger) =>
        {
            logger.LogInformation("Execute get customer");

            var customerId = await mediator.Send(new CreateCustomerCommand(data.Email));

            await mediator.Publish(new CustomerCreatedDomainEvent(1, "debit", "123", "John Doe", DateTime.UtcNow));

            return Results.Ok(customerId);
        })
        .WithName("CreateCustomer")
        .WithOpenApi();
    }
}
