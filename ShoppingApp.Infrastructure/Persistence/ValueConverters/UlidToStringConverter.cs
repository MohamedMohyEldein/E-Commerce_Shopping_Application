using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ShoppingApp.Infrastructure.Persistence.ValueConverters
{
    public class UlidToStringConverter : ValueConverter<Ulid, string>
    {
        public UlidToStringConverter() : base(
            v => v.ToString(),
            v => Ulid.Parse(v))
        {
        }
    }
}

