using FluentValidation;
using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class DeleteCustomerHandler(IUnitOfWork unitOfWork, ILogger<DeleteCustomerHandler> logger)
    : IRequestHandler<DeleteCustomerCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<DeleteCustomerHandler> _logger = logger;

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogCategory.CommandHandler, "Validating request");
        new DeleteCustomerCommandValidator().ValidateAndThrow(request);

        _logger.LogInformation(LogCategory.CommandHandler, "Deleting customer");

        await _unitOfWork.CustomerRepository.DeleteAsync(request.Id);
        await _unitOfWork.SaveAsync();

        _logger.LogInformation(LogCategory.CommandHandler, "Customer deleted");
    }
}
