using FluentValidation;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api.Application.Commands;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator(ILogger<UpdateCustomerCommandValidator> logger)
    {
        RuleFor(command => command.Id).NotEmpty();
        RuleFor(command => command.Data.Email).NotEmpty().EmailAddress();

        logger.LogTrace(LogCategory.ValidatorUpdateCustomer, "INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
