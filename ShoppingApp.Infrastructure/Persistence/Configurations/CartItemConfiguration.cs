using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Infrastructure.Persistence.ValueConverters;

namespace ShoppingApp.Infrastructure.Persistence.Configurations
{
    internal class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        private static readonly UlidToStringConverter _ulidConverter = new();

        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.Property(p => p.Id).HasConversion(_ulidConverter).HasMaxLength(26).IsUnicode(false);
            builder.Property(p => p.CartId).HasConversion(_ulidConverter).HasMaxLength(26).IsUnicode(false);
            builder.Property(p => p.ProductVariantId).HasConversion(_ulidConverter).HasMaxLength(26).IsUnicode(false);
            builder.ToTable("Cart_item").HasKey(p => p.Id);
        }
    }
}
