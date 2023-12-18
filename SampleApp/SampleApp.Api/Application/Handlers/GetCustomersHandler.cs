using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class GetCustomersHandler(IUnitOfWork unitOfWork, ILogger<GetCustomersHandler> logger)
    : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerInfo>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<GetCustomersHandler> _logger = logger;

    public async Task<IEnumerable<CustomerInfo>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogCategory.QueryHandler, "---- Retrieving customers");

        return await _unitOfWork.CustomerRepository.GetAllAsync();
    }
}
