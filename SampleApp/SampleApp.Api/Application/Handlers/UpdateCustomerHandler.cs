using FluentValidation;
using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateCustomerHandler> _logger;
    private readonly IValidator<UpdateCustomerCommand> _validator;

    public UpdateCustomerHandler(
        IUnitOfWork unitOfWork,
        ILogger<UpdateCustomerHandler> logger,
        IValidator<UpdateCustomerCommand> validator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        _logger.LogInformation(LogCategory.CommandHandler, "---- Update customer");

        await _unitOfWork.CustomerRepository.UpdateAsync(request.Id, request.Data);
        await _unitOfWork.SaveAsync();
    }
}