using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Infrastructure.Persistence.ValueConverters;

namespace ShoppingApp.Infrastructure.Persistence.Configurations
{
    internal class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        private static readonly UlidToStringConverter _ulidConverter = new();

        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.Property(p => p.Id).HasConversion(_ulidConverter).HasMaxLength(26).IsUnicode(false);
            builder.ToTable("Wish_list").HasKey(p => p.Id);
            builder.HasMany(e => e.Items)
                .WithOne(e => e.Wishlist)
                .HasForeignKey(p => p.WishlistId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
