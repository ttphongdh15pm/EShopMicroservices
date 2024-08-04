using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    internal class CusomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(c => c.Id)
                .HasConversion(customerId => customerId.Value, dbId => CustomerId.Of(dbId));
            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(255).IsRequired();
            builder.HasIndex(c => c.Email).IsUnique();
        }
    }
}
