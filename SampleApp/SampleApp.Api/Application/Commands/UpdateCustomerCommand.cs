using FluentValidation;
using MediatR;
using SampleApp.Domain.Customer.Entities;

namespace SampleApp.Api.Application.Commands;

public record UpdateCustomerCommand : IRequest
{
    public long Id { get; private set; }
    public CustomerInfo Data { get; private set; }

    public UpdateCustomerCommand(long id, CustomerInfo data)
    {
        Id = id;
        Data = data;

        new UpdateCustomerCommandValidator().ValidateAndThrow(this);
    }
}
