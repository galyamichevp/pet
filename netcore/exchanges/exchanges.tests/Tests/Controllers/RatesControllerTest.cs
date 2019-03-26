
using exchanges.server.Controllers;
using exchanges.server.Entities.Models;
using exchanges.server.Infrastructure.Repositories;
using exchanges.server.Infrastructure.Repositories.Interfaces;
using exchanges.tests.Tests.Controllers.Loggers;
using exchanges.tests.Tests.Controllers.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Xunit;

namespace exchanges.tests.Tests.Controllers
{
    public class RatesControllerTest
    {
        DistributedCacheFake _distributedCacheFake;
        IRedisRepository _redisRepository;
        RatesController _ratesController;
        ILogger<RatesController> _logger;

        public RatesControllerTest()
        {
            _logger = new RatesControllerLogerFake();
            _distributedCacheFake = new DistributedCacheFake();
            _redisRepository = new RedisRepository(_distributedCacheFake, null);
            _ratesController = new RatesController(_redisRepository, _logger);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOk()
        {
            var model = new RatesRequest
            {
                From = "USD",
                To = "RUB"
            };

            var response = await _ratesController.Get(model);
            var result = response.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<RatesResponse>(result.Value);
            Assert.Equal(model.From, (result.Value as RatesResponse).From);
            Assert.NotEmpty((result.Value as RatesResponse).Rates);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsNoContent()
        {
            var model = new RatesRequest
            {
                From = "NONE",
                To = "USD"
            };

            var response = await _ratesController.Get(model);
            var result = response.Result as NoContentResult;

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsServerError()
        {
            var model = new RatesRequest
            {
                From = "",
                To = ""
            };

            var response = await _ratesController.Get(model);
            var result = response.Result as StatusCodeResult;

            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task GetAll_WhenCalled_ReturnsOk()
        {
            var model = new RatesRequest
            {
                From = "USD"
            };

            var response = await _ratesController.GetAll(model);
            var result = response.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<RatesResponse>(result.Value);
            Assert.Equal(model.From, (result.Value as RatesResponse).From);
            Assert.NotEmpty((result.Value as RatesResponse).Rates);
        }

        [Fact]
        public async Task GetAll_WhenCalled_ReturnsNoContent()
        {
            var model = new RatesRequest
            {
                From = "NONE"
            };

            var response = await _ratesController.GetAll(model);
            var result = response.Result as NoContentResult;

            Assert.IsType<NoContentResult>(result);
        }
    }
}
