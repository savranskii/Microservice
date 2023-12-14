using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class SearchCustomerHandler : IRequestHandler<SearchCustomerQuery, CustomerInfo?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SearchCustomerHandler> _logger;

    public SearchCustomerHandler(IUnitOfWork unitOfWork, ILogger<SearchCustomerHandler> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<CustomerInfo?> Handle(SearchCustomerQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogCategory.QueryHandler, "---- Retrieving customers by filters");

        var item = await _unitOfWork.CustomerRepository.GetByEmailAsync(request.Data.Email);
        _unitOfWork.Dispose();
        return item;
    }
}