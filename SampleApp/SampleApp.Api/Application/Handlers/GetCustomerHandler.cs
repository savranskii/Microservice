using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Customer.Repositories;

namespace SampleApp.Api.Application.Handlers;

public class GetCustomerHandler(ICustomerRepository customerRepository) : IRequestHandler<GetCustomerByEmailQuery, Customer?>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<Customer?> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _customerRepository.GetByEmailAsync(request.Email);
    }
}