using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Customer.Repositories;

namespace SampleApp.Api.Application.Handlers;

public class SearchCustomerHandler(ICustomerRepository customerRepository, ILogger<SearchCustomerHandler> logger) : IRequestHandler<SearchCustomerQuery, Customer?>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly ILogger<SearchCustomerHandler> _logger = logger;

    public async Task<Customer?> Handle(SearchCustomerQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving customer");

        return await _customerRepository.GetByEmailAsync(request.Data.Email);
    }
}