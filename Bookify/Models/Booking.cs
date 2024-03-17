using Bookify.Models.Abstractions;
using Bookify.Services;
using System.Runtime.CompilerServices;

namespace Bookify.Models
{
    public class Booking : Entity
    {
        public Booking() { }

        private Booking(
            Guid id,
            Guid apartmentId,
            Guid userId,
            DateTime dateFrom,
            DateTime dateTo,
            decimal priceForPeriod,
            decimal cleaningFee,
            decimal amenitiesUpCharge,
            decimal totalPrice,
            BookingStatus status,
            DateTime createdOnUtc) : base(id)
        {
            ApartmentId = apartmentId;
            UserId = userId;
            DateFrom = dateFrom;
            DateTo = dateTo;
            PriceForPeriod = priceForPeriod;
            CleaningFee = cleaningFee;
            AmenitiesUpCharge = amenitiesUpCharge;
            TotalPrice = totalPrice;
            Status = status;
            CreatedOnUtc = createdOnUtc;
        }

        public Guid ApartmentId { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime DateFrom { get; private set; }
        public DateTime DateTo { get; private set; }
        public decimal PriceForPeriod { get; private set; }
        public decimal CleaningFee { get; private set; }
        public decimal AmenitiesUpCharge { get; private set; }
        public decimal TotalPrice { get; private set; }
        public BookingStatus Status { get; private set; }
        public DateTime CreatedOnUtc {  get; private set; } 
        public DateTime? ConfirmedOnUtc {  get; private set; } 
        public DateTime? RejectedOnUtc {  get; private set; } 
        public DateTime? CompletedOnUtc {  get; private set; } 
        public DateTime? CancelledOnUtc {  get; private set; } 


        public static Booking Reserve(
            PricingService pricingService,
            Apartment apartment,
            Guid userId,
            DateOnly dateFrom,
            DateOnly dateTo)
        {
            var pricingDetails = pricingService.CalculatePrice(apartment, dateFrom, dateTo);

            var booking = new Booking(
                Guid.NewGuid(),
                apartment.Id,
                userId,
                dateFrom.ToDateTime(TimeOnly.MinValue),
                dateTo.ToDateTime(TimeOnly.MinValue),
                pricingDetails.PriceForPeriod,
                pricingDetails.CleaningFee,
                pricingDetails.AmenitiesUpCharge,
                pricingDetails.TotalPrice,
                BookingStatus.Reserved,
                DateTime.Now);

            return booking;
        }
    }
}