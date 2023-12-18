using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class SearchCustomerHandler(IUnitOfWork unitOfWork, ILogger<SearchCustomerHandler> logger)
    : IRequestHandler<SearchCustomerQuery, CustomerInfo?>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<SearchCustomerHandler> _logger = logger;

    public async Task<CustomerInfo?> Handle(SearchCustomerQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogCategory.QueryHandler, "Retrieving customers by filters");

        return await _unitOfWork.CustomerRepository.GetByEmailAsync(request.Data.Email);
    }
}
