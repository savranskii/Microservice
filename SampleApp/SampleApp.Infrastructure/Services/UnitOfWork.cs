using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.DbContexts;
using SampleApp.Infrastructure.Repositories;

namespace SampleApp.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly  CustomerContext _context;
    private ICustomerRepository? _customerRepository;

    public ICustomerRepository CustomerRepository
    {
        get
        {
            _customerRepository ??= new CustomerRepository(_context);
            return _customerRepository;
        }
    }

    public UnitOfWork(DbContextOptions<CustomerContext> options)
    {
       _context = new CustomerContext(options);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
