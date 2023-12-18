using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class GetCustomerHandler(IUnitOfWork unitOfWork, ILogger<GetCustomerHandler> logger)
    : IRequestHandler<GetCustomerQuery, CustomerInfo?>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<GetCustomerHandler> _logger = logger;

    public async Task<CustomerInfo?> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogCategory.QueryHandler, "Retrieving customer");

        return await _unitOfWork.CustomerRepository.GetByIdAsync(request.Id);
    }
}
