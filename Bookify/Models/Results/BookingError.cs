namespace Bookify.Models.Results
{
    public static class BookingError
    {
        public static Error Date = new(
            "Booking.Date",
            "DateTo (end date) precedes date From (start date)");

        public static Error Overlap = new(
            "Booking.Overlap",
            "Booking dates overlap with active bookings");
    }
}
