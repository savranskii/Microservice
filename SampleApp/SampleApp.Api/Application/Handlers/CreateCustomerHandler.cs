using FluentValidation;
using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.Constants;

namespace SampleApp.Api.Application.Handlers;

public class CreateCustomerHandler(ICustomerRepository repo, ILogger<CreateCustomerHandler> logger)
    : IRequestHandler<CreateCustomerCommand, CustomerInfo>
{
    private readonly ICustomerRepository _repo = repo;
    private readonly ILogger<CreateCustomerHandler> _logger = logger;

    public async Task<CustomerInfo> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogCategory.CommandHandler, "Validating request");
        new CreateCustomerCommandValidator().ValidateAndThrow(request);

        var customer = new CustomerInfo(request.Email);

        _logger.LogInformation(LogCategory.CommandHandler, "Creating customer");

        await _repo.CreateAsync(customer);
        await _repo.UnitOfWork.SaveEntitiesAsync();

        _logger.LogInformation(LogCategory.CommandHandler, "Customer created");

        return customer;
    }
}
