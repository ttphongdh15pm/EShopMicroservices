using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(c => c.Id).HasConversion(customerId => customerId.Value, dbId => ProductId.Of(dbId));
            builder.Property(prod => prod.Name).HasMaxLength(100).IsRequired();
        }
    }
}
