using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Domain.Identities;

namespace ShoppingApp.Infrastructure.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {

            builder.Property(p => p.FullName)
                .HasMaxLength(100).IsRequired();

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
