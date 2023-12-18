using FluentValidation;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api.Application.Commands;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator(ILogger<CreateCustomerCommandValidator> logger)
    {
        RuleFor(command => command.Email).NotEmpty().EmailAddress().WithMessage("Please specify a valid email");

        logger.LogTrace(LogCategory.ValidatorCreateCustomer, "INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
