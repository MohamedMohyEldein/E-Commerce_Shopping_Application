using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Identities;
using ShoppingApp.Infrastructure.Persistence.ValueConverters;

namespace ShoppingApp.Infrastructure.Persistence.Configurations
{
    internal sealed class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        private static readonly UlidToStringConverter _ulidConverter = new();

        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.Property(p => p.Id)
                .HasConversion(_ulidConverter)
                .HasMaxLength(26)
                .IsUnicode(false);
        }
    }

    internal sealed class IdentityUserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<Ulid>>
    {
        private static readonly UlidToStringConverter _ulidConverter = new();

        public void Configure(EntityTypeBuilder<IdentityUserClaim<Ulid>> builder)
        {
            builder.Property(p => p.UserId)
                .HasConversion(_ulidConverter)
                .HasMaxLength(26)
                .IsUnicode(false);
        }
    }

    internal sealed class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Ulid>>
    {
        private static readonly UlidToStringConverter _ulidConverter = new();

        public void Configure(EntityTypeBuilder<IdentityUserRole<Ulid>> builder)
        {
            builder.Property(p => p.UserId)
                .HasConversion(_ulidConverter)
                .HasMaxLength(26)
                .IsUnicode(false);

            builder.Property(p => p.RoleId)
                .HasConversion(_ulidConverter)
                .HasMaxLength(26)
                .IsUnicode(false);
        }
    }

    internal sealed class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<Ulid>>
    {
        private static readonly UlidToStringConverter _ulidConverter = new();

        public void Configure(EntityTypeBuilder<IdentityUserLogin<Ulid>> builder)
        {
            builder.Property(p => p.UserId)
                .HasConversion(_ulidConverter)
                .HasMaxLength(26)
                .IsUnicode(false);
        }
    }

    internal sealed class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<Ulid>>
    {
        private static readonly UlidToStringConverter _ulidConverter = new();

        public void Configure(EntityTypeBuilder<IdentityUserToken<Ulid>> builder)
        {
            builder.Property(p => p.UserId)
                .HasConversion(_ulidConverter)
                .HasMaxLength(26)
                .IsUnicode(false);
        }
    }

    internal sealed class IdentityRoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<Ulid>>
    {
        private static readonly UlidToStringConverter _ulidConverter = new();

        public void Configure(EntityTypeBuilder<IdentityRoleClaim<Ulid>> builder)
        {
            builder.Property(p => p.RoleId)
                .HasConversion(_ulidConverter)
                .HasMaxLength(26)
                .IsUnicode(false);
        }
    }
}