using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, Customer?>
{
    private readonly IUnitOfWork _unitOfWOrk;
    private readonly ILogger<GetCustomerHandler> _logger;

    public GetCustomerHandler(IUnitOfWork unitOfWork, ILogger<GetCustomerHandler> logger)
    {
        _unitOfWOrk = unitOfWork;
        _logger = logger;
    }

    public async Task<Customer?> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving customer");

        return await _unitOfWOrk.CustomerRepository.GetByIdAsync(request.Id);
    }
}
