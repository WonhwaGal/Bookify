using Bookify.Models;

namespace Bookify.Services
{
    public class PricingService
    {
        public PricingDetails CalculatePrice(
            Apartment apartment, 
            DateOnly dateFrom, 
            DateOnly dateTo)
        {
            var priceForPeriod = (dateTo.DayNumber - dateFrom.DayNumber) * apartment.Price;

            var percentageUpCharge = apartment.Amenities?.Sum(amenity =>
            amenity switch
            {
                Amenity.GardenView or Amenity.MountainView => 0.5m,
                Amenity.AirConditioning => 0.01m,
                Amenity.Parking => 0.01m,
                _ => 0
            });

            decimal amenitiesUpCharge = 0;
            if (percentageUpCharge != null && percentageUpCharge > 0)
                amenitiesUpCharge = priceForPeriod * percentageUpCharge.Value;

            decimal totalPrice = 0;
            totalPrice += priceForPeriod;
            totalPrice += apartment.CleaningFee;
            totalPrice += amenitiesUpCharge;

            return new PricingDetails
            {
                PriceForPeriod = priceForPeriod,
                AmenitiesUpCharge = amenitiesUpCharge,
                CleaningFee = apartment.CleaningFee,
                TotalPrice = totalPrice
            };
        }
    }
}
