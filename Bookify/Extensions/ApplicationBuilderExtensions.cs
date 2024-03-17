using Bogus;
using Bookify.Infrastructure;
using Bookify.Infrastructure.Services;
using Bookify.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }

        public static void SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
            using var connection = sqlConnectionFactory.CreateConnection();

            var faker = new Faker();

            #region Create Apartments
            List<dynamic> apartments = [];
            for (int i = 0; i < 100; i++)
            {
                string apartmentType = i % 3 == 0 ? "Apartment" : "House";
                var randomAmenities = $"[{faker.Random.Byte(1,3)}, {faker.Random.Byte(4, 6)}, {faker.Random.Byte(7, 10)}]";
                string finalAmenities = faker.Random.Bool() ? "[]" : randomAmenities;

                apartments.Add(new
                {
                    Id = Guid.NewGuid(),
                    Name = $"{faker.Hacker.Noun().ToUpper()} {apartmentType}",
                    Description = faker.Lorem.Text(),
                    Address = $"{faker.Address.City()}, {faker.Address.StreetAddress()}",
                    Price = faker.Random.Decimal(50, 1000),
                    CleaningFee = faker.Random.Decimal(25, 200),
                    Amenities = finalAmenities,
                    LastBookedOnUtc = faker.Date.Past(2, DateTime.Now)
                });
            }

            const string sql = """
                INSERT INTO apartments
                (Id, Name, Description, Address, Price, CleaningFee, Amenities,LastBookedOnUtc)
                VALUES(@Id, @Name, @Description, @Address, @Price, @CleaningFee, @Amenities, @LastBookedOnUtc)
                """;

            connection.Execute(sql, apartments);
            #endregion

            #region Create Clients
            List<dynamic> clients = [];
            for (int i = 0; i < 200; i++)
            {
                clients.Add(new
                {
                    Id = Guid.NewGuid(),
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    Email = faker.Internet.Email(),
                    Password = faker.Internet.Password()
                });
            }

            const string sql_clients = """
                INSERT INTO users
                (Id, FirstName, LastName, Email, Password)
                VALUES(@Id, @FirstName, @LastName, @Email, @Password)
                """;

            connection.Execute(sql_clients, clients);
            #endregion

            #region Create Bookings
            List<dynamic> bookings = [];
            for (int i = 0; i < 80; i++)
            {
                var apartment = apartments[faker.Random.Int(0, apartments.Count - 1)];
                var userId = clients[faker.Random.Int(0, clients.Count - 1)].Id;

                // starting 5 years ago and up to 1 year in the future
                var dateFrom = faker.Date.Past(5, faker.Date.Future(1, DateTime.Today));
                var totalDays = faker.Random.Int(1, 40);
                var dateTo = faker.Date.Between(dateFrom, dateFrom.AddDays(totalDays));
                var createDate = faker.Date.Future(1, dateFrom);

                var priceForPeriod = apartment.Price * totalDays;
                var amenitiesCharge = faker.Random.Decimal(50, 500);

                BookingStatus randomStatus = faker.Random.Enum<BookingStatus>();
                if (dateFrom > DateTime.Today && randomStatus == BookingStatus.Completed)
                    randomStatus = BookingStatus.Confirmed;
                if(dateTo < DateTime.Today 
                    && (randomStatus != BookingStatus.Rejected || randomStatus != BookingStatus.Cancelled))
                        randomStatus = BookingStatus.Completed;

                DateTime? confirmed = null;
                DateTime? cancelled = null;
                DateTime? completed = null;
                DateTime? rejected = null;

                switch (randomStatus)
                {
                    case BookingStatus.Rejected:
                        rejected = createDate.AddDays(1);
                        break;
                    case BookingStatus.Cancelled:
                        confirmed = createDate.AddDays(1);
                        cancelled = faker.Date.Between(createDate, DateTime.Today);
                        break;
                    case BookingStatus.Completed:
                        confirmed = createDate.AddDays(1);
                        completed = dateTo;
                        break;
                    case BookingStatus.Confirmed:
                        confirmed = createDate;
                        break;
                }

                bookings.Add(new
                {
                    Id = Guid.NewGuid(),
                    ApartmentId = apartment.Id,
                    UserId = userId,
                    DateFrom = dateFrom,
                    DateTo = dateTo,
                    PriceForPeriod = priceForPeriod,
                    CleaningFee = apartment.CleaningFee,
                    AmenitiesUpCharge = amenitiesCharge,
                    TotalPrice = priceForPeriod + apartment.CleaningFee + amenitiesCharge,
                    Status = randomStatus,
                    CreatedOnUtc = createDate,
                    ConfirmedOnUtc = confirmed,
                    RejectedOnUtc = rejected,
                    CompletedOnUtc = completed,
                    CancelledOnUtc = cancelled
                });
            }

            const string sql_bookings = """
                INSERT INTO bookings
                (Id, ApartmentId, UserId, DateFrom, DateTo, PriceForPeriod, CleaningFee, AmenitiesUpCharge, 
                TotalPrice, Status, CreatedOnUtc, ConfirmedOnUtc, RejectedOnUtc, CompletedOnUtc, CancelledOnUtc)
                VALUES
                (@Id, @ApartmentId, @UserId, @DateFrom, @DateTo, @PriceForPeriod, @CleaningFee, @AmenitiesUpCharge, 
                @TotalPrice, @Status, @CreatedOnUtc, @ConfirmedOnUtc, @RejectedOnUtc, @CompletedOnUtc, @CancelledOnUtc)
                """;

            connection.Execute(sql_bookings, bookings);
            #endregion

            #region Create Reviews
            List<object> reviews = [];
            // 1 to 1 relation
            dynamic[] completedBookings = bookings
                .Where(x => x.Status == BookingStatus.Completed)
                .ToArray();

            for (int i = 0; i < completedBookings.Length; i++)
            {
                if (i % 10 == 0)
                    continue;

                var booking = completedBookings[i];
                var rating = faker.Random.Int(1, 5);
                if(rating <= 2)
                    rating = faker.Random.Int(1, 10) % 2 == 0 ? 5 : rating;

                reviews.Add(new
                {
                    Id = Guid.NewGuid(),
                    Rating = rating,
                    Comment = faker.Rant.Review(),
                    BookingId = booking.Id,
                    ApartmentId = booking.ApartmentId,
                    UserId = booking.UserId,
                    CreatedOnUtc = faker.Date.Between(booking.DateTo, DateTime.Today)
                });
            }

            const string sql_reviews = """
                INSERT INTO reviews
                (Id, Rating, Comment, BookingId, ApartmentId, UserId, CreatedOnUtc)
                VALUES
                (@Id, @Rating, @Comment, @BookingId, @ApartmentId, @UserId, @CreatedOnUtc)
                """;

            connection.Execute(sql_reviews, reviews);
            #endregion
        }
    }
}
