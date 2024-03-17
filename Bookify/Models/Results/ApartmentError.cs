namespace Bookify.Models.Results
{
    public static class ApartmentError
    {
        public static Error NotFound = new(
            "Apartment.Found",
            "The apartment with the specified identifier was not found");

        public static Error Exists = new(
            "Apartment.Address",
            "The apartment with the specified address is already in the system");
    }
}
