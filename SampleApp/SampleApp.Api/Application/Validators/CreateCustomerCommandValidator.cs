using FluentValidation;
using SampleApp.Api.Application.Commands;

namespace SampleApp.Api.Validators;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator(ILogger<CreateCustomerCommandValidator> logger)
    {
        RuleFor(command => command.Email).NotEmpty().EmailAddress().WithMessage("Please specify a valid email");

        logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
