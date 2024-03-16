using Bookify.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bookify.Models.Requests
{
    public class SearchApartmentRequest
    {
        /// <summary>
        /// Дата заезда в формате: 2024-01-01
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Дата заезда должна быть указана")]
        [CompareDateWith(isBiggerThanCurrent: true, ErrorMessage = "Дата заезда не может быть меньше или равна текущей")]
        public DateOnly? StartDate { get; set; }

        /// <summary>
        /// Дата выезда в формате: 2024-01-01
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Дата выезда должна быть указана")]
        [CompareDateWith(other: nameof(StartDate), isBiggerThanOther: true,
            ErrorMessage = "Промежуток времени указан неверно: дата заезда больше или равна дате выезда")]
        public DateOnly? EndDate { get; set; }

        /// <summary>
        /// Максимальная стоимость
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        ///  1 - Wifi;
        ///  2 - Кондиционер;
        ///  3 - Парковка;
        ///  4 - Домашними животные;
        ///  5 - Бассейн;
        ///  6 - Спортзал;
        ///  7 - СПА;
        ///  8 - Терраса;
        ///  9 - Вид на горы;
        ///  10 - Вид на сад;
        /// </summary>
        public ICollection<Amenity>? NecessaryAmenities { get; set; }
    }
}