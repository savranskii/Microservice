using FluentValidation;

namespace SampleApp.Api.Application.Commands;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator(ILogger<UpdateCustomerCommandValidator> logger)
    {
        RuleFor(command => command.Id).NotEmpty();
        RuleFor(command => command.Data.Email).NotEmpty().EmailAddress();

        logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
