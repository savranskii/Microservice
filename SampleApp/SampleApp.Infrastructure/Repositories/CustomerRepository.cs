using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.DbContexts;

namespace SampleApp.Infrastructure.Repositories;

public class CustomerRepository(CustomerContext context) : ICustomerRepository
{
    private readonly CustomerContext _context = context;
    
    public async Task CreateAsync(Customer item)
    {
        _context.Customers.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var customer = await _context.Customers.FindAsync(id) ?? throw new ArgumentException("Invalid ID");
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<Customer?> GetByIdAsync(long id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task UpdateAsync(long id, Customer item)
    {
        var customer = await _context.Customers.FindAsync(id) ?? throw new ArgumentException("Invalid ID");

        customer.Email = item.Email;

        await _context.SaveChangesAsync();
    }
}
