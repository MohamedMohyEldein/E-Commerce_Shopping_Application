﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Infrastructure.Persistence.ValueConverters;

namespace ShoppingApp.Infrastructure.Persistence.Configurations
{
    internal class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        private static readonly UlidToStringConverter _ulidConverter = new();

        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.Property(p => p.Id).HasConversion(_ulidConverter).HasMaxLength(26).IsUnicode(false);
            builder.Property(p => p.UserId).HasConversion(_ulidConverter).HasMaxLength(26).IsUnicode(false);
            builder.ToTable("Cart").HasKey(p => p.Id);
            builder.HasMany(e => e.Items)
                .WithOne(e => e.Cart)
                .HasForeignKey(p => p.CartId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
