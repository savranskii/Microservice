using FluentValidation;
using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class CreateCustomerHandler(
    IUnitOfWork unitOfWork,
    ILogger<CreateCustomerHandler> logger,
    IValidator<CreateCustomerCommand> validator) : IRequestHandler<CreateCustomerCommand, long>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<CreateCustomerHandler> _logger = logger;
    private readonly IValidator<CreateCustomerCommand> _validator = validator;

    public async Task<long> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var customer = new CustomerInfo(request.Email);

        _logger.LogInformation(LogCategory.CommandHandler, "---- Customer created");

        await _unitOfWork.CustomerRepository.CreateAsync(customer);
        await _unitOfWork.SaveAsync();

        return customer.Id;
    }
}
