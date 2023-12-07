using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Domain.Customer.Repositories;

namespace SampleApp.Api.Application.Handlers;

public class UpdateCustomerHandler(ICustomerRepository customerRepository, ILogger<UpdateCustomerHandler> logger) : IRequestHandler<UpdateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly ILogger<UpdateCustomerHandler> _logger = logger;

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Update customer");

        await _customerRepository.UpdateAsync(request.Id, request.Data);
    }
}