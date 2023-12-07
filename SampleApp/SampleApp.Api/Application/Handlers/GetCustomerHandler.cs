using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Customer.Repositories;

namespace SampleApp.Api.Application.Handlers;

public class GetCustomerHandler(ICustomerRepository customerRepository, ILogger<GetCustomerHandler> logger) : IRequestHandler<GetCustomerQuery, Customer?>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly ILogger<GetCustomerHandler> _logger = logger;

    public async Task<Customer?> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving customer");

        return await _customerRepository.GetByIdAsync(request.Id);
    }
}
