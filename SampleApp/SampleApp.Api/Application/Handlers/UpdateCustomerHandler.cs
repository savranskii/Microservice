using FluentValidation;
using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api.Application.Handlers;

public class UpdateCustomerHandler(ICustomerRepository repo, ILogger<UpdateCustomerHandler> logger)
    : IRequestHandler<UpdateCustomerCommand>
{
    private readonly ICustomerRepository _repo = repo;
    private readonly ILogger<UpdateCustomerHandler> _logger = logger;

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogCategory.CommandHandler, "Validating request");
        new UpdateCustomerCommandValidator().ValidateAndThrow(request);

        _logger.LogInformation(LogCategory.CommandHandler, "Updating customer");

        await _repo.UpdateAsync(request.Id, new(request.Data.Email));
        await _repo.UnitOfWork.SaveEntitiesAsync();

        _logger.LogInformation(LogCategory.CommandHandler, "Customer updated");
    }
}
