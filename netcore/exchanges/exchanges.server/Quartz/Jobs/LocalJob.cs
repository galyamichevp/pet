using exchanges.server.Entities;
using exchanges.server.Infrastructure.Providers.Interfaces;
using exchanges.server.Infrastructure.Repositories.Interfaces;
using exchanges.server.Resources;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Threading.Tasks;

namespace exchanges.server.Quartz.Jobs
{
    internal class LocalJob : IJob
    {
        private readonly IExchangeProviderFactory _exchangeProviderFactory;
        private readonly IMongoRepository _mongoRepository;
        private readonly IRedisRepository _redisRepository;
        private readonly ILogger<LocalJob> _logger;

        public LocalJob(IExchangeProviderFactory exchangeProviderFactory, 
            IMongoRepository mongoRepository,
            IRedisRepository redisRepository,
            ILogger<LocalJob> logger)
        {
            _exchangeProviderFactory = exchangeProviderFactory;
            _mongoRepository = mongoRepository;
            _redisRepository = redisRepository;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var provider = _exchangeProviderFactory.GetProvider(Constants.Providers.LocalProvider);

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
