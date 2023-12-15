﻿using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, CustomerInfo?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCustomerHandler> _logger;

    public GetCustomerHandler(IUnitOfWork unitOfWork, ILogger<GetCustomerHandler> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<CustomerInfo?> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogCategory.QueryHandler, "---- Retrieving customer");

        return await _unitOfWork.CustomerRepository.GetByIdAsync(request.Id);
    }
}
