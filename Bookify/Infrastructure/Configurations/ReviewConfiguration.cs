using Bookify.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("reviews");
            builder.HasKey(review => review.Id);

            builder.Property(review => review.Rating)
                .IsRequired();
            builder.Property(review => review.Comment);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(review => review.UserId);
            builder.HasOne<Apartment>()
                .WithMany()
                .HasForeignKey(review => review.ApartmentId);
            builder.HasOne<Booking>()
                .WithOne()
                .HasForeignKey<Review>(review => review.BookingId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}