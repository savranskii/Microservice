using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class SearchCustomerHandler : IRequestHandler<SearchCustomerQuery, Customer?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SearchCustomerHandler> _logger;

    public SearchCustomerHandler(IUnitOfWork unitOfWork, ILogger<SearchCustomerHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Customer?> Handle(SearchCustomerQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("---- Retrieving customer");

        var item = await _unitOfWork.CustomerRepository.GetByEmailAsync(request.Data.Email);
        _unitOfWork.Dispose();
        return item;
    }
}