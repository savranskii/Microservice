using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class CreateCustomerHandler(IUnitOfWork unitOfWork, ILogger<CreateCustomerHandler> logger)
    : IRequestHandler<CreateCustomerCommand, CustomerInfo>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<CreateCustomerHandler> _logger = logger;

    public async Task<CustomerInfo> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new CustomerInfo(request.Email);

        _logger.LogInformation(LogCategory.CommandHandler, "Customer created");

        await _unitOfWork.CustomerRepository.CreateAsync(customer);
        await _unitOfWork.SaveAsync();

        _logger.LogInformation(LogCategory.CommandHandler, "Customer entity saved");

        return customer;
    }
}
