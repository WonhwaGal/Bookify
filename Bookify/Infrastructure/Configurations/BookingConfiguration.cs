using Bookify.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("bookings");
            builder.HasKey(booking => booking.Id);

            builder.Property(booking => booking.PriceForPeriod);
            builder.Property(booking => booking.CleaningFee);
            builder.Property(booking => booking.AmenitiesUpCharge);
            builder.Property(booking => booking.TotalPrice);
            builder.Property(booking => booking.DateFrom);
            builder.Property(booking => booking.DateTo);

            builder.HasOne<Apartment>()
                .WithMany()
                .HasForeignKey(booking => booking.ApartmentId);
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(booking => booking.UserId);
        }
    }
}
