using Bookify.Models.Abstractions;
using Bookify.Services;

namespace Bookify.Models
{
    public class Apartment : Entity
    {
        public Apartment() { }
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

        public static Apartment Create(
            ApartmentSettingService settingService, 
            string name, 
            string address, 
            decimal price)
        {
            var appartmentSettings = settingService.SetUpApartment(name, address, price);

            return new Apartment(
                Guid.NewGuid(),
                appartmentSettings.Name,
                appartmentSettings.Description,
                appartmentSettings.Address,
                appartmentSettings.Price,
                appartmentSettings.CleaningFee,
                appartmentSettings.Amenities);
        }

        public string Name { get; private set; }

        public string Description { get; private set; }
        
        public string Address { get; private set; }
        
        public decimal Price { get; private set; }
        
        public decimal CleaningFee { get; private set; }
        
        public DateTime? LastBookedOnUtc { get; internal set; }
        
        public ICollection<Amenity>? Amenities { get; private set; } = new List<Amenity>();
    }
}