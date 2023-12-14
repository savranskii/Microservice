using FluentValidation;
using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteCustomerHandler> _logger;
    private readonly IValidator<DeleteCustomerCommand> _validator;

    public DeleteCustomerHandler(
        IUnitOfWork unitOfWork,
        ILogger<DeleteCustomerHandler> logger,
        IValidator<DeleteCustomerCommand> validator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        await _unitOfWork.CustomerRepository.DeleteAsync(request.Id);
        await _unitOfWork.SaveAsync();
        _unitOfWork.Dispose();

        _logger.LogInformation(LogCategory.CommandHandler, "---- Customer deleted");
    }
}
