using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.Contexts;

namespace SampleApp.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerContext _context;

    public CustomerRepository(CustomerContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CustomerInfo item)
    {
        _context.Customers.Add(item);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(long id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer is not null)
        {
            _context.Customers.Remove(customer);
        }
    }

    public async Task<CustomerInfo?> GetByEmailAsync(string email)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<CustomerInfo?> GetByIdAsync(long id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<IEnumerable<CustomerInfo>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task UpdateAsync(long id, CustomerInfo item)
    {
        var customer = await _context.Customers.FindAsync(id) ?? throw new ArgumentException("Invalid ID");
        customer.SetEmail(item.Email);
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
