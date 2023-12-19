using FluentValidation;
using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class UpdateCustomerHandler(IUnitOfWork unitOfWork, ILogger<UpdateCustomerHandler> logger)
    : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<UpdateCustomerHandler> _logger = logger;

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogCategory.CommandHandler, "Validating request");
        new UpdateCustomerCommandValidator().ValidateAndThrow(request);

        _logger.LogInformation(LogCategory.CommandHandler, "Updating customer");

        await _unitOfWork.CustomerRepository.UpdateAsync(request.Id, request.Data);
        await _unitOfWork.SaveAsync();

        _logger.LogInformation(LogCategory.CommandHandler, "Customer updated");
    }
}
