using MediatR;
using SampleApp.Api.Application.Queries;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.Application.Handlers;

public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, IEnumerable<Customer>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCustomersHandler> _logger;

    public GetCustomersHandler(IUnitOfWork unitOfWOrk, ILogger<GetCustomersHandler> logger)
    {
        _unitOfWork = unitOfWOrk;
        _logger = logger;
    }

    public async Task<IEnumerable<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("---- Retrieving customer");

        var items = await _unitOfWork.CustomerRepository.GetAllAsync();
        _unitOfWork.Dispose();
        return items;
    }
}
