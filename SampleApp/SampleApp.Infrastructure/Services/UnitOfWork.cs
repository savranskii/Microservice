using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Customer.Repositories;
using SampleApp.Infrastructure.Contexts;
using SampleApp.Infrastructure.Repositories;

namespace SampleApp.Infrastructure.Services;

public class UnitOfWork(DbContextOptions<CustomerContext> options, IMediator mediator) : IUnitOfWork
{
    private readonly CustomerContext _context = new(options);
    private readonly IMediator _mediator = mediator;
    private ICustomerRepository? _customerRepository;

    public ICustomerRepository CustomerRepository
    {
        get
        {
            _customerRepository ??= new CustomerRepository(_context);
            return _customerRepository;
        }
    }

    public async Task SaveAsync()
    {
        await _mediator.DispatchDomainEventsAsync(_context);

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
