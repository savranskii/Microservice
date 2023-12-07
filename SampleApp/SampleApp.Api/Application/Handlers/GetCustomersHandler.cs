using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Customer.Repositories;

namespace SampleApp.Api.Application.Handlers;

public class GetCustomersHandler(ICustomerRepository customerRepository, ILogger<GetCustomersHandler> logger) : IRequestHandler<GetCustomersQuery, IEnumerable<Customer>>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly ILogger<GetCustomersHandler> _logger = logger;

    public async Task<IEnumerable<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving customer");

        return await _customerRepository.GetAllAsync();
    }
}
