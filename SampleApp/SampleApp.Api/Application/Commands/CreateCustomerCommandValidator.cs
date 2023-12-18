using FluentValidation;

namespace SampleApp.Api.Application.Commands;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(command => command.Email).NotEmpty().EmailAddress().WithMessage("Please specify a valid email");
    }
}
