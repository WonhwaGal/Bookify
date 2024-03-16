using Bookify.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace Bookify.Models.Requests
{
    public class ReserveBookingRequest
    {
        /// <summary>
        /// Идентификатор аппартмента
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Идентификатор аппартмента должен быть указан")]
        public Guid? ApartmentId { get; set; }

        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Идентификатор клиента должен быть указан")]
        public Guid? UserId { get; set; }

        /// <summary>
        /// Дата заезда
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Дата заезда должна быть указана")]
        [CompareDateWith(isBiggerThanCurrent: true, ErrorMessage = "Дата заезда не может быть меньше или равна текущей")]
        public DateOnly? DateFrom { get; set; }

        /// <summary>
        /// Дата выезда
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Дата выезда должна быть указана")]
        [CompareDateWith(other: nameof(DateFrom), 
            isBiggerThanOther: true, ErrorMessage = "Промежуток времени указан неверно: дата заезда больше или равна дате выезда")]
        public DateOnly? DateTo { get; set; }
    }
}
