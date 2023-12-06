using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Customer.Repositories;

namespace SampleApp.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private static int _lastId = 1;
    private readonly static List<Customer> _customers = [];

    public async Task CreateAsync(Customer item)
    {
        item.Id = _lastId;
        _customers.Add(item);
        
        _lastId++;

        await Task.CompletedTask;
    }

    public Task DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await Task.FromResult(_customers.FirstOrDefault(c => c.Email == email));
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
