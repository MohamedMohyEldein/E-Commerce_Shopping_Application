using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Domain.Identities;
using ShoppingApp.Infrastructure.Persistence.ValueConverters;

namespace ShoppingApp.Infrastructure.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        private static readonly UlidToStringConverter _ulidConverter = new();

        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // Configure Id to be varchar(26) not nvarchar(26)
            builder.Property(p => p.Id)
                .HasConversion(_ulidConverter)
                .HasMaxLength(26)
                .IsUnicode(false);

            builder.Property(p => p.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(p => p.Cart)
                .WithOne(p => p.User)
                .HasForeignKey<Cart>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Wishlist)
                .WithOne(p => p.User)
                .HasForeignKey<Wishlist>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Orders)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
