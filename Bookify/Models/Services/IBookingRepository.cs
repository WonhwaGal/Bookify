
using Bookify.Models.Results;

namespace Bookify.Models.Services
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Result<Guid> Reserve(Guid apartmentId, Guid userId, DateOnly dateFrom, DateOnly dateTo);

        bool IsOverlapping(Apartment apartment, DateOnly dateFrom, DateOnly dateTo);
        
        Result<IReadOnlyList<Booking>> GetByPer(
            Guid? userId, DateOnly? startDate, DateOnly? endDate, BookingStatus? bookingStatus);
    }
}
