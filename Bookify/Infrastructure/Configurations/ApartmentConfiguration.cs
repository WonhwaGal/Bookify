using Bookify.Infrastructure.Data;
using Bookify.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations
{
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.ToTable("apartments");
            builder.HasKey(apartment => apartment.Id);

            builder.Property(apartment => apartment.Name)
                .HasMaxLength(200);
            builder.Property(apartment => apartment.Address)
                .HasMaxLength(500);
            builder.Property(apartment => apartment.Description)
                .HasMaxLength(2000);
            builder.Property(apartment => apartment.Price);

            builder.Property(apartment => apartment.CleaningFee);

            var converter = new EnumCollectionJsonValueConverter<Amenity>();
            builder
                .Property(apartment => apartment.Amenities)
                .HasConversion(converter);
        }
    }
}