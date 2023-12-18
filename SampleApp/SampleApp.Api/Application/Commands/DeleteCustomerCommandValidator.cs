using FluentValidation;

namespace SampleApp.Api.Application.Commands;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();
    }
}
