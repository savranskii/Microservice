using FluentValidation;

namespace SampleApp.Api.Application.Commands;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator(ILogger<DeleteCustomerCommandValidator> logger)
    {
        RuleFor(command => command.Id).NotEmpty();

        logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
