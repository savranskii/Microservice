using FluentValidation;
using MediatR;

namespace SampleApp.Api.Application.Commands;

public record DeleteCustomerCommand : IRequest
{
    public long Id { get; private set; }

    public DeleteCustomerCommand(long id)
    {
        Id = id;

        new DeleteCustomerCommandValidator().ValidateAndThrow(this);
    }
}
