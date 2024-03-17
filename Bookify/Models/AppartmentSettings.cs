namespace Bookify.Models
{
    public class ApartmentSettings
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public decimal CleaningFee { get; set; }
        public ICollection<Amenity>? Amenities { get; set; } = new List<Amenity>();
    }
}