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
        /// Описание
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Описание аппартаментов должно быть указано")]
        [StringLength(maximumLength: int.MaxValue, MinimumLength = 10, 
            ErrorMessage = "Описание аппартаментов не должно содержать менее 10 символов")]
        public string? Description { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Адрес аппартаментов должен быть указан")]
        [StringLength(maximumLength: 500, MinimumLength = 10,
            ErrorMessage = "Адрес аппартаментов должен содержать от 10 до 500 символов")]
        public string? Address { get; set; }

        /// <summary>
        /// Дополнительные удобства
        /// </summary>
        public ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
    }
}