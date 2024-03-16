using Bookify.Models.Abstractions;

namespace Bookify.Models
{
    public class Apartment : Entity
    {
        public Apartment(
            Guid id,
            string name,
            string description,
            string address,
            decimal price,
            decimal cleaningFee,
            ICollection<Amenity> amenities) : base(id)
        {
            Name = name;
            Description = description;
            Address = address;
            Price = price;
            CleaningFee = cleaningFee;
            Amenities = amenities;
        }

        public Apartment() { }

        private Apartment(Guid id) : base(id) { }

        public static Apartment Create() => new (Guid.NewGuid());

        public string Name { get; private set; }

        public string Description { get; private set; }
        
        public string Address { get; private set; }
        
        public decimal Price { get; private set; }
        
        public decimal CleaningFee { get; private set; }
        
        public DateTime? LastBookedOnUtc { get; internal set; }
        
        public ICollection<Amenity>? Amenities { get; private set; } = new List<Amenity>();
    }
}