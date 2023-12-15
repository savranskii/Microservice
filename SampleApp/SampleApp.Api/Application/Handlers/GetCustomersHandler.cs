using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerInfo>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCustomersHandler> _logger;

    public GetCustomersHandler(IUnitOfWork unitOfWork, ILogger<GetCustomersHandler> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CustomerInfo>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogCategory.QueryHandler, "---- Retrieving customers");

        return await _unitOfWork.CustomerRepository.GetAllAsync();
    }
}
