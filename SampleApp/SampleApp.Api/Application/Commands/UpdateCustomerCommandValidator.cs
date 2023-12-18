using FluentValidation;

namespace SampleApp.Api.Application.Commands;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();
        RuleFor(command => command.Data.Email).NotEmpty().EmailAddress();
    }
}
