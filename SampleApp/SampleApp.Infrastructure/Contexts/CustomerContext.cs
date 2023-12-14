using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.EntityConfigurations;

namespace SampleApp.Infrastructure.Contexts;

public class CustomerContext : DbContext
{
    public DbSet<CustomerInfo> Customers => Set<CustomerInfo>();

    public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }
}
