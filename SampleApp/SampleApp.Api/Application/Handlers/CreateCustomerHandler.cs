using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, long>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCustomerHandler> _logger;

    public CreateCustomerHandler(IUnitOfWork unitOfWork, ILogger<CreateCustomerHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<long> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Email = request.Email,
        };

        _logger.LogInformation("Customer created");

        await _unitOfWork.CustomerRepository.CreateAsync(customer);
        await _unitOfWork.SaveAsync();

        return customer.Id;
    }
}
