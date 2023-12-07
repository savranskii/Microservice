using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Domain.Customer.Repositories;

namespace SampleApp.Api;

public class DeleteCustomerHandler(ICustomerRepository repository, ILogger<DeleteCustomerHandler> logger) : IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _repository = repository;
    private readonly ILogger<DeleteCustomerHandler> _logger = logger;

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);

        _logger.LogInformation("Customer deleted");
    }
}
