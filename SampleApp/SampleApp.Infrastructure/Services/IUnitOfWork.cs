using SampleApp.Domain.Customer.Repositories;

namespace SampleApp.Infrastructure.Services;

public interface IUnitOfWork : IDisposable
{
    ICustomerRepository CustomerRepository { get; }
    Task SaveAsync();
}
