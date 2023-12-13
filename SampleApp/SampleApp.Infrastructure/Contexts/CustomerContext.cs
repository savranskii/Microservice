using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.EntityConfigurations;

namespace SampleApp.Infrastructure.Contexts;

public class CustomerContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();

    public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }
}
