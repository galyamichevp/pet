using exchanges.server.Infrastructure.Providers.Interfaces;
using exchanges.server.Infrastructure.Repositories.Interfaces;
using exchanges.server.Quartz.Jobs;
using exchanges.tests.Tests.Jobs.Providers;
using exchanges.tests.Tests.Jobs.Repositories;
using Quartz;
using System;
using System.Threading.Tasks;
using Xunit;

namespace exchanges.tests.Tests.Jobs
{
    public class LocalJobTest
    {
        IJob _job;
        IMongoRepository _mongoRepository;
        IRedisRepository _redisRepository;
        ExchangeProviderFactoryFake _exchangeProviderFactory;

        public LocalJobTest()
        {
            _mongoRepository = new MongoRepositoryFake();
            _redisRepository = new RedisRepositoryFake();
            _exchangeProviderFactory = new ExchangeProviderFactoryFake();

            _job = new LocalJob(_exchangeProviderFactory, _mongoRepository, _redisRepository, null);
        }

        [Fact]
        public async Task Execute_WhenCalled_ProcessWithProvider()
        {
            _exchangeProviderFactory.ThrowException = false;

            await _job.Execute(null);
        }

        [Fact]
        public async Task Execute_WhenCalled_ProcessWithoutProvider()
        {
            _exchangeProviderFactory.ThrowException = true;

            var ex = Record.ExceptionAsync(() => _job.Execute(null));

            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex.Result);
        }
    }
}
