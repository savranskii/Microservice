using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Domain.Seeds;
using SampleApp.Infrastructure.EntityConfigurations;

namespace SampleApp.Infrastructure.Contexts;

public sealed class CustomerContext(DbContextOptions<CustomerContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<CustomerInfo> Customers => Set<CustomerInfo>();

    public async Task<bool> SaveEntitiesAsync(CancellationToken ct = default)
    {
        await base.SaveChangesAsync(ct);

        return true;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }
}
