using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api.Application.Handlers;

public class GetCustomersHandler(ICustomerRepository repo, ILogger<GetCustomersHandler> logger)
    : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerInfo>>
{
    private readonly ICustomerRepository _repo = repo;
    private readonly ILogger<GetCustomersHandler> _logger = logger;

    public async Task<IEnumerable<CustomerInfo>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogCategory.QueryHandler, "Retrieving customers");

        return await _repo.GetAllAsync();
    }
}
