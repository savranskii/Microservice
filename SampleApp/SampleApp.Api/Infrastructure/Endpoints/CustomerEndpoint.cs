using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Api.Application.Commands;
using SampleApp.Api.Application.Models;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api.Infrastructure.Endpoints;

public class CustomerEndpoint
{
    public static async Task<Results<Ok<CustomerInfo>, NotFound>> GetAsync(
        [FromRoute] long id,
        [FromServices] IMediator mediator,
        [FromServices] ILogger<CustomerEndpoint> logger)
    {
        logger.LogInformation(LogCategory.CustomerEndpoint, "Execute get by id customer");

        var customer = await mediator.Send(new GetCustomerQuery(id));

        return customer is null ? TypedResults.NotFound() : TypedResults.Ok(customer);
    }

    public static async Task<Ok<IEnumerable<CustomerInfo>>> GetAllAsync(
        [FromServices] IMediator mediator,
        [FromServices] ILogger<CustomerEndpoint> logger)
    {
        logger.LogInformation(LogCategory.CustomerEndpoint, "Execute get all customer");

        var customers = await mediator.Send(new GetCustomersQuery());

        return TypedResults.Ok(customers);
    }

    public static async Task<Results<Ok<CustomerInfo>, NotFound>> SearchAsync(
        [FromBody] SearchCustomerRequest data,
        [FromServices] IMediator mediator,
        [FromServices] ILogger<CustomerEndpoint> logger)
    {
        logger.LogInformation(LogCategory.CustomerEndpoint, "Execute search customer");

        var customer = await mediator.Send(new SearchCustomerQuery(data));

        return customer is null ? TypedResults.NotFound() : TypedResults.Ok(customer);
    }

    public static async Task<Ok<CustomerInfo>> CreateAsync(
        [FromBody] CreateCustomerRequest data,
        [FromServices] IMediator mediator,
        [FromServices] ILogger<CustomerEndpoint> logger)
    {
        logger.LogInformation(LogCategory.CustomerEndpoint, "Execute create customer");

        var customer = await mediator.Send(new CreateCustomerCommand(data.Email));

        return TypedResults.Ok(customer);
    }

    public static async Task<NoContent> UpdateAsync(
        [FromRoute] long id,
        [FromBody] UpdateCustomerRequest data,
        [FromServices] IMediator mediator,
        [FromServices] ILogger<CustomerEndpoint> logger)
    {
        logger.LogInformation(LogCategory.CustomerEndpoint, "Execute update customer");

        await mediator.Send(new UpdateCustomerCommand(id, data));

        return TypedResults.NoContent();
    }

    public static async Task<NoContent> DeleteAsync(
        [FromRoute] long id,
        [FromServices] IMediator mediator,
        [FromServices] ILogger<CustomerEndpoint> logger)
    {
        logger.LogInformation(LogCategory.CustomerEndpoint, "Execute delete customer");

        await mediator.Send(new DeleteCustomerCommand(id));

        return TypedResults.NoContent();
    }
}
