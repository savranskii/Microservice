using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Customer.Entities;

namespace SampleApp.Infrastructure.DbContexts;

public class CustomerContext : DbContext
{
    public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
}
