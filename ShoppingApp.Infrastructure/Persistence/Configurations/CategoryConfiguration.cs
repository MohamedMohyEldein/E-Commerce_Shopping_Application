using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Infrastructure.Persistence.ValueConverters;

namespace ShoppingApp.Infrastructure.Persistence.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        private static readonly UlidToStringConverter _ulidConverter = new();

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p => p.Id).HasConversion(_ulidConverter).HasMaxLength(26).IsUnicode(false);
            builder.ToTable("Category").HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(1000);
            builder.HasMany(e => e.Products)
                .WithOne(e => e.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
