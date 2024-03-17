using System.ComponentModel.DataAnnotations;

namespace Bookify.Models.Requests
{
    public class AddApartmentRequest
    {
        /// <summary>
        /// Название
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Название аппартаментов должно быть указано")]
        [StringLength(40, ErrorMessage = "Название аппартаментов не должно содержать более 40 символов")]
        public string? Name { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Адрес аппартаментов должен быть указан")]
        [StringLength(maximumLength: 500, MinimumLength = 10,
            ErrorMessage = "Адрес аппартаментов должен содержать от 10 до 500 символов")]
        public string? Address { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Цена аппартаментов должна быть указана")]
        [Range(50, 1000, 
            ErrorMessage = "Стоимость апартаментов должна находиться в диапазоне от 50 до 1000")]
        public decimal? Price { get; set; }

        /// <summary>
        /// Дополнительные удобства
        /// </summary>
        public ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
    }
}