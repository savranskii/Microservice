using FluentValidation;
using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, long>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCustomerHandler> _logger;
    private readonly IValidator<CreateCustomerCommand> _validator;

    public CreateCustomerHandler(
        IUnitOfWork unitOfWork,
        ILogger<CreateCustomerHandler> logger,
        IValidator<CreateCustomerCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<long> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var customer = new Customer(request.Email);

        _logger.LogInformation("---- Customer created");

        await _unitOfWork.CustomerRepository.CreateAsync(customer);
        await _unitOfWork.SaveAsync();
        _unitOfWork.Dispose();

        return customer.Id;
    }
}
