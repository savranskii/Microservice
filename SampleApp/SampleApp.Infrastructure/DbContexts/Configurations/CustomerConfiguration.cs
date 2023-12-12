using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleApp.Domain.Customer.Entities;

namespace SampleApp.Infrastructure.DbContexts.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");
        
        builder
            .Ignore(c => c.DomainEvents)
            .Ignore(c => c.Version);

        builder.Property(c => c.Email).IsRequired();
        builder.HasIndex(c => c.Email).IsUnique();
    }
}
