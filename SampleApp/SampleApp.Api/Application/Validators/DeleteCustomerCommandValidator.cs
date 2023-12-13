using FluentValidation;
using SampleApp.Api.Application.Commands;

namespace SampleApp.Api.Validators;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator(ILogger<DeleteCustomerCommandValidator> logger)
    {
        RuleFor(command => command.Id).NotEmpty();

        logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
