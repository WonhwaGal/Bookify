using Bookify.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace Bookify.Models.Requests
{
    public class FilterReviewsRequest
    {
        /// <summary>
        /// Идентификатор аппартамента
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Идентификатор аппартамента должен быть указан")]
        public Guid ApartmentId { get; init; }

        /// <summary>
        /// Оценка от 1 до 5
        /// </summary>
        [Range(1, 5, ErrorMessage = "Оценка отзыва должна находиться в диапазоне от 1 до 5")]
        public byte? Rating { get; init; }

        /// <summary>
        /// Начальная дата поиска в формате: 2024-01-01
        /// </summary>
        public DateOnly? StartDate { get; init; }

        /// <summary>
        /// Конечная дата поиска в формате: 2024-01-01
        /// </summary>
        [CompareDateWith(other: nameof(StartDate), isBiggerThanOther: true,
            ErrorMessage = "Промежуток времени указан неверно: начальная дата больше или равна конечной")]
        public DateOnly? EndDate { get; init; }
    }
}