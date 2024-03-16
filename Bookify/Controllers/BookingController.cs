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
    /// Представляет набор методов, отвечающих за работу с бронированиями
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController(IBookingRepository bookingRepository) : ControllerBase
    {
        /// <summary>
        /// Получение информации о бронированиях пользователя, опционально по датам и статусу
        /// </summary>
        /// <param name="request">Набор параметров, необходимых для фильтрации бронирований</param>
        /// <returns>Результат фильтрации бронирований</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpGet("filter")]
        [ProducesResponseType(typeof(IReadOnlyList<Booking>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [TypeFilter(typeof(ValidationFilterAttribute), Arguments = new object[] { -105 })]
        public IActionResult FilterBookings([FromQuery] FilterBookingsRequest request)
        {
            var query = bookingRepository.GetByPer(
                request.UserId,
                request.StartDate,
                request.EndDate,
                request.BookingStatus);
            return Ok(query.Value);
        }

        /// <summary>
        /// Создание нового бронирования
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST/ToDo
        ///     {
        ///         "apartmentId": "[идентификатор аппартаментов]",
        ///         "userId": "[идентификатор клиента]",
        ///         "startDate": "дата заезда в формате [2024-02-12]",
        ///         "endDate": "дата выезда в формате [2024-02-12]"
        ///     }
        /// </remarks>
        /// <param name="request">Набор параметров, необходимых для создания бронирования клиента</param>
        /// <returns>Результат создания нового бронирования</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("reserve")]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [TypeFilter(typeof(ValidationFilterAttribute), Arguments = new object[] { -106 })]
        public IActionResult ReceiveBooking([FromBody] ReserveBookingRequest request)
        {
            var result = bookingRepository.Reserve(
                request.ApartmentId.Value,
                request.UserId.Value,
                request.DateFrom.Value,
                request.DateTo.Value);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(result.Error);
        }
    }
}