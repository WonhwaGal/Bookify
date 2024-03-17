using System.ComponentModel.DataAnnotations;

namespace Bookify.Models.Requests
{
    public class CreateReviewRequest
    {
        /// <summary>
        /// Идентификатор бронирования
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Идентификатор услуги бронирования должен быть указан")]
        public Guid BookingId { get; set; }

        /// <summary>
        /// Оценка от 1 до 5
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Отзыв должен содержать оценку")]
        [Range(1,5, ErrorMessage = "Оценка отзыва должна находиться в диапазоне от 1 до 5")]
        public int Rating { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string? Comment { get; set; }
    }
}