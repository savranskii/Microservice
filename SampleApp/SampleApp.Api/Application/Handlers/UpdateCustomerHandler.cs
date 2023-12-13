﻿using FluentValidation;
using MediatR;
using SampleApp.Api.Application.Commands;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateCustomerHandler> _logger;
    private readonly IValidator<UpdateCustomerCommand> _validator;

    public UpdateCustomerHandler(
        IUnitOfWork unitOfWork,
        ILogger<UpdateCustomerHandler> logger,
        IValidator<UpdateCustomerCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        _logger.LogInformation("---- Update customer");

        await _unitOfWork.CustomerRepository.UpdateAsync(request.Id, request.Data);
        await _unitOfWork.SaveAsync();
        _unitOfWork.Dispose();
    }
}