using Bogus;
using Bookify.Models;

namespace Bookify.Services
{
    public class ApartmentSettingService
    {
        public ApartmentSettings SetUpApartment(string name, string address, decimal price)
        {
            var faker = new Faker();

            var randomNumber = faker.Random.Byte(10, 50);
            string apartmentType = randomNumber % 2 == 0 ? "Apartment" : "House";
            var apartmentName = $"{name} {apartmentType}";

            var description = faker.Lorem.Text();
            var cleaningFee = price * randomNumber / 100;

            var amenities = new List<Amenity>();
            for (int i = 1; i <= 10; i++)
            {
                if (faker.Random.Bool())
                    amenities.Add((Amenity)i);
            }

            return new ApartmentSettings
            {
                Name = apartmentName,
                Description = description,
                Address = address,
                Price = price,
                CleaningFee = cleaningFee,
                Amenities = amenities
            };
        }
    }
}