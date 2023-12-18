using FluentValidation;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api.Application.Commands;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator(ILogger<DeleteCustomerCommandValidator> logger)
    {
        RuleFor(command => command.Id).NotEmpty();

        logger.LogTrace(LogCategory.ValidatorDeleteCustomer, "INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
