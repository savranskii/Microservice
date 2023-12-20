using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api.Application.Handlers;

public class GetCustomerHandler(ICustomerRepository repo, ILogger<GetCustomerHandler> logger)
    : IRequestHandler<GetCustomerQuery, CustomerInfo?>
{
    private readonly ICustomerRepository _repo = repo;
    private readonly ILogger<GetCustomerHandler> _logger = logger;

    public async Task<CustomerInfo?> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogCategory.QueryHandler, "Retrieving customer");

        return await _repo.GetByIdAsync(request.Id);
    }
}
