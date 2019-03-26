using exchanges.server.Entities;
using exchanges.server.Entities.Models;
using exchanges.server.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace exchanges.server.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly IRedisRepository _redisRepository;
        private readonly ILogger<RatesController> _logger;

        public RatesController(IRedisRepository redisRepository, ILogger<RatesController> logger)
        {
            _redisRepository = redisRepository;
            _logger = logger;
        }

        /// <summary>
        /// Получение курсов пар валют доступных для продаваемой валюты
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns>Информация о курсах обмена для продаваемой валюты</returns>
        /// <response code="200">Курсы обмена найдены</response>
        /// <response code="204">Курсы обмена не найдены</response>
        /// <response code="500">Произошла ошибка при поиске информации</response>
        [HttpGet("{from}")]
        [ProducesResponseType(typeof(RatesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RatesResponse>> GetAll([FromRoute]RatesRequest model)
        {
            try
            {
                var data = await _redisRepository.GetExchangeInfo(Enum.Parse<ExchangeType>(model.From.ToUpperInvariant()), ExchangeType.NONE);

                var result = data.ToRatesResponse();

                if (!result.Rates.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);


                return Ok(ex);
                //return StatusCode(500);
            }
        }

        /// <summary>
        /// Получение курса для сторого определенной пары валют
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns>Информация о курсе обмена для запрашиваемой пары валют</returns>
        /// <response code="200">Курс обмена найден</response>
        /// <response code="204">Курс обмена не найден</response>
        /// <response code="500">Произошла ошибка при поиске информации</response>
        [HttpGet("{from}/{to}")]
        [ProducesResponseType(typeof(RatesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RatesResponse>> Get([FromRoute] RatesRequest model)
        {
            try
            {
                var data = await _redisRepository.GetExchangeInfo(Enum.Parse<ExchangeType>(model.From.ToUpperInvariant()), Enum.Parse<ExchangeType>(model.To.ToUpperInvariant()));
                var result = data.ToRatesResponse();

                if (!result.Rates.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);

                return StatusCode(500);
            }
        }
    }
}
