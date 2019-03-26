using exchanges.server.Entities;
using exchanges.server.Infrastructure.Providers.Interfaces;
using exchanges.server.Infrastructure.Repositories.Interfaces;
using exchanges.server.Resources;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace exchanges.server.Quartz.Jobs
{
    internal class OERJob : IJob
    {
        private readonly IExchangeProviderFactory _exchangeProviderFactory;
        private readonly IMongoRepository _mongoRepository;
        private readonly IRedisRepository _redisRepository;
        private readonly ILogger<OERJob> _logger;

        public OERJob(IExchangeProviderFactory exchangeProviderFactory, 
            IMongoRepository mongoRepository,
            IRedisRepository redisRepository,
            ILogger<OERJob> logger)
        {
            _exchangeProviderFactory = exchangeProviderFactory;
            _mongoRepository = mongoRepository;
            _redisRepository = redisRepository;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var provider = _exchangeProviderFactory.GetProvider(Constants.Providers.OERProvider);

            if (provider == null)
                return;

            foreach (var baseType in ExchangeExtensions.GetExchangeTypes())
            {
                try
                {
                    var exchangeInfo = provider.GetExchangeInfo(baseType);

                    await _mongoRepository.SaveExchangeInfo(exchangeInfo.ToStoreExchangeInfo(provider.ProviderName));
                    await _redisRepository.SetCacheExchangeInfo(exchangeInfo.ToCacheExchangeInfo(provider.ExpiredInMsec), provider.ExpiredInMsec);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, ex.Message);
                }
            }
        }
    }
}
