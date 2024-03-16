namespace Bookify.Models.Results
{
    public static class ApartmentError
    {
        public static Error NotFound = new(
            "Apartment.Found",
            "The apartment with the specified identifier was not found");
    }
}
