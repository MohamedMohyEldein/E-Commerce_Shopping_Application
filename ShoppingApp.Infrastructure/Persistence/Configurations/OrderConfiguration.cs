using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Infrastructure.Persistence.ValueConverters;

namespace ShoppingApp.Infrastructure.Persistence.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        private static readonly UlidToStringConverter _ulidConverter = new();

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(p => p.Id).HasConversion(_ulidConverter).HasMaxLength(26).IsUnicode(false);
            builder.ToTable("Order").HasKey(p => p.Id);
            //builder.Property(p => p.TotalAmount).HasPrecision(18, 2);
            builder.Property(p => p.Status).HasConversion<string>().IsRequired().IsUnicode(false);
            builder.Property(p => p.OrderDate).IsRequired();
            builder.HasMany(e => e.Items)
                .WithOne(e => e.Order)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
