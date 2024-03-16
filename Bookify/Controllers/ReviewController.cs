using Bookify.Filters;
using Bookify.Models;
using Bookify.Models.Results;
using Bookify.Models.Requests;
using Bookify.Models.Services;
using Bookify.Models.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bookify.Controllers
{
    /// <summary>
    /// Представляет набор методов, отвечающих за работу с отзывами пользователей
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController(IReviewRepository reviewRepository): ControllerBase
    {
        /// <summary>
        /// Размещение нового отзыва на бронирование
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST/ToDo
        ///     {
        ///         "bookingId": "[идентификатор бронирования]",
        ///         "rating": "[оценка бронирования : от 1 до 5]",
        ///         "comment": "[комментарий клиента]"
        ///     }
        ///     
        /// </remarks>
        /// <param name="request">Набор параметров, необходимых для создания отзыва на услугу бронирования</param>
        /// <returns>Результат создания нового отзыва</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("post-review")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [TypeFilter(typeof(ValidationFilterAttribute), Arguments = new object[] { -103 })]
        public IActionResult PostReview([FromBody] CreateReviewRequest request)
        {
            return Ok(Guid.NewGuid());
        }

        /// <summary>
        /// Поиск отзывов об аппартаменте, опционально по временному периоду и оценке
        /// </summary>
        /// <param name="request">Набор параметров, необходимых для предоставления выборки отзывов</param>
        /// <returns>Результат проведения поиска отзывов</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet("filter-reviews")]
        [ProducesResponseType(typeof(IReadOnlyList<Review>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [TypeFilter(typeof(ValidationFilterAttribute), Arguments = new object[] { -104 })]
        public IActionResult FilterReviews([FromQuery] FilterReviewsRequest request)
        {
            var query = reviewRepository.GetByPer(
                request.ApartmentId,
                request.StartDate,
                request.EndDate,
                request.Rating);
            return Ok(query.Value);
        }
    }
}