using Bookify.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace Bookify.Models.Requests
{
    public class FilterBookingsRequest
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Идентификатор клиента должен быть указан")]
        public Guid? UserId { get; set; }

        /// <summary>
        ///  1 - Зарезервировано;
        ///  2 - Подтверждено;
        ///  3 - Отменено системой (отказ);
        ///  4 - Отменено клиентом;
        ///  5 - Услуга полностью выполнена;
        /// </summary>
        public BookingStatus? BookingStatus { get; set; }

        /// <summary>
        /// Начальная дата поиска в формате: 2024-01-01
        /// </summary>
        [CompareDateWith(other: nameof(EndDate), isBiggerThanOther: false,
            ErrorMessage = "Промежуток времени указан неверно: начальная дата больше или равна конечной")]
        public DateOnly? StartDate { get; set; }

        /// <summary>
        /// Конечная дата поиска в формате: 2024-01-01
        /// </summary>
        public DateOnly? EndDate { get; set; }
    }
}