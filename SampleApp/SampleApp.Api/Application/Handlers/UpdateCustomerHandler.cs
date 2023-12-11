using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateCustomerHandler> _logger;

    public UpdateCustomerHandler(IUnitOfWork unitOfWork, ILogger<UpdateCustomerHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Update customer");

        await _unitOfWork.CustomerRepository.UpdateAsync(request.Id, request.Data);
        await _unitOfWork.SaveAsync();
    }
}