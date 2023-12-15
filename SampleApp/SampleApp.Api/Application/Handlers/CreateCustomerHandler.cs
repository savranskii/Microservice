using FluentValidation;
using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Constants;
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
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

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
