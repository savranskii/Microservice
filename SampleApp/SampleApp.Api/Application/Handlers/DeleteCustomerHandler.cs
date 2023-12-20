using FluentValidation;
using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api.Application.Handlers;

public class DeleteCustomerHandler(ICustomerRepository repo, ILogger<DeleteCustomerHandler> logger)
    : IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _repo = repo;
    private readonly ILogger<DeleteCustomerHandler> _logger = logger;

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogCategory.CommandHandler, "Validating request");
        new DeleteCustomerCommandValidator().ValidateAndThrow(request);

        _logger.LogInformation(LogCategory.CommandHandler, "Deleting customer");

        await _repo.DeleteAsync(request.Id);
        await _repo.UnitOfWork.SaveEntitiesAsync();

        _logger.LogInformation(LogCategory.CommandHandler, "Customer deleted");
    }
}
