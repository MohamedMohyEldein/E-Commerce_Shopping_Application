using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Infrastructure.Persistence.ValueConverters;

namespace ShoppingApp.Infrastructure.Persistence.Configurations
{
    internal class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        private static readonly UlidToStringConverter _ulidConverter = new();

        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.Property(p => p.Id).HasConversion(_ulidConverter).HasMaxLength(26).IsUnicode(false);
            builder.Property(p => p.ProductId).HasConversion(_ulidConverter).HasMaxLength(26).IsUnicode(false);
            builder.ToTable("Product_variant").HasKey(p => p.Id);
            builder.Property(p => p.Color).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Size).IsRequired().HasMaxLength(255);

            builder.HasMany(e => e.CartItems)
                .WithOne(e => e.ProductVariant)
                .HasForeignKey(p => p.ProductVariantId);

            builder.HasMany(e => e.OrderItems)
                .WithOne(e => e.ProductVariant)
                .HasForeignKey(p => p.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
