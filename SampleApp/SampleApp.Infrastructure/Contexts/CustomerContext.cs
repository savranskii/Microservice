using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.EntityConfigurations;

namespace SampleApp.Infrastructure.Contexts;

public sealed class CustomerContext(DbContextOptions<CustomerContext> options) : DbContext(options)
{
    public DbSet<CustomerInfo> Customers => Set<CustomerInfo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }
}
