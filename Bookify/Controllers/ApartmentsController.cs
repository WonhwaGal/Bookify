using Bookify.Filters;
using Bookify.Models;
using Bookify.Models.Results;
using Bookify.Models.Requests;
using Bookify.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bookify.Controllers
{
    /// <summary>
    /// Представляет набор методов по работе с аппартаментами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController(IApartmentRepository apartmentRepository) : ControllerBase
    {
        /// <summary>
        /// Поиск аппартаментов по датам заселения и дополнительным параметрам
        /// </summary>
        /// <param name="request">Набор параметров, необходимых для поиска аппартаментов</param>
        /// <returns>Результат поиска аппартаментов</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [ProducesResponseType(typeof(IReadOnlyList<Apartment>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [TypeFilter(typeof(ValidationFilterAttribute), Arguments = new object[] { -107 })]
        [HttpGet("search")]
        public IActionResult SearchApartments([FromQuery]SearchApartmentRequest request)
        {
            var query = apartmentRepository.GetByPer(
                request.StartDate!.Value,
                request.EndDate!.Value,
                request.MaxPrice);
            return Ok(query.Value);
        }

        /// <summary>
        /// Добавление нового аппартамента
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST/ToDo
        ///     {
        ///         "name": "[название аппартаментов]",
        ///         "description": "[описание аппартаментов]",
        ///         "address": "[адрес аппартаментов]",
        ///         "amenities": "дополнительные удобства в формате [ 1, 2, 3]"
        ///     }
        ///
        /// 1 - Wifi;  2 - Кондиционер;  3 - Парковка;  4 - Домашние животные;  5 - Бассейн;
        ///   6 - Спортзал;  7 - СПА;  8 - Терраса;  9 - Вид на горы;  10 - Вид на сад;
        /// </remarks>
        /// <param name="request">Набор параметров, необходимых для добавления аппартамента</param>
        /// <returns>Результат создания нового аппартамента</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [TypeFilter(typeof(ValidationFilterAttribute), Arguments = new object[] { -108 })]
        [HttpPost("add-apartment")]
        public IActionResult AddApartment([FromBody]AddApartmentRequest request)
        {
            apartmentRepository.Create(Apartment.Create());
            return Ok(Guid.NewGuid());
        }
    }
}