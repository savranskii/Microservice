using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Customer.Repositories;

namespace SampleApp.Api.Application.Handlers;

public class CreateCustomerHandler(ICustomerRepository customerRepository) : IRequestHandler<CreateCustomerCommand, long>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<long> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            // Id = _orders.Count + 1,
            Email = request.Email,
        };

        await _customerRepository.CreateAsync(customer);

        return customer.Id;
    }
}
