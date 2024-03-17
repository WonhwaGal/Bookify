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

        public static Error NotFound = new(
            "Booking.Found",
            "Booking with the specified identifier was not found");

        public static Error NotCompleted = new(
            "Booking.NotCompleted",
            "Only completed bookings can have reviews");

        public static Error ExistingReview = new(
            "Booking.ExistingReview",
            "This booking already has a published review");
    }
}
