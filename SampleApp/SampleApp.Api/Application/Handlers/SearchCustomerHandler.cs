using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api.Application.Handlers;

public class SearchCustomerHandler(ICustomerRepository repo, ILogger<SearchCustomerHandler> logger)
    : IRequestHandler<SearchCustomerQuery, CustomerInfo?>
{
    private readonly ICustomerRepository _repo = repo;
    private readonly ILogger<SearchCustomerHandler> _logger = logger;

    public async Task<CustomerInfo?> Handle(SearchCustomerQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogCategory.QueryHandler, "Retrieving customers by filters");

        return await _repo.GetByEmailAsync(request.Data.Email);
    }
}
