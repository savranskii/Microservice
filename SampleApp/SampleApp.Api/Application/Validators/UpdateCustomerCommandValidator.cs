using FluentValidation;
using SampleApp.Api.Application.Commands;

namespace SampleApp.Api.Validators;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator(ILogger<UpdateCustomerCommandValidator> logger)
    {
        RuleFor(command => command.Id).NotEmpty();
        RuleFor(command => command.Data.Email).NotEmpty().EmailAddress();

        logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
