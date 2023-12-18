using FluentValidation;
using MediatR;
using SampleApp.Domain.Customer.Entities;

namespace SampleApp.Api.Application.Commands;

public record CreateCustomerCommand : IRequest<CustomerInfo>
{
    public string Email { get; private set; }

    public CreateCustomerCommand(string email)
    {
        Email = email;

        new CreateCustomerCommandValidator().ValidateAndThrow(this);
    }
}
