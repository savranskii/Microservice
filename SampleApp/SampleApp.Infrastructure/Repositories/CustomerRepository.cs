using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Customer.Repositories;

namespace SampleApp.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    public Task CreateAsync(Customer item)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(long id, Customer item)
    {
        throw new NotImplementedException();
    }
}
