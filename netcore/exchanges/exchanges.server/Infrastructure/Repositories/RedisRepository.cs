using exchanges.server.Entities;
using exchanges.server.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ZeroFormatter;

[assembly: InternalsVisibleTo("exchanges.tests")]

namespace exchanges.server.Infrastructure.Repositories
{
    internal class RedisRepository : IRedisRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<RedisRepository> _logger;

        public RedisRepository(IDistributedCache distributedCache, ILogger<RedisRepository> logger)
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public async Task<CacheExchangeInfo> GetExchangeInfo(ExchangeType @base, ExchangeType target)
        {
            try
            {
                var exchangeInfoCache = await _distributedCache.GetAsync(@base.ToString());

                if (exchangeInfoCache == null)
                    return new CacheExchangeInfo
                    {
                        BaseType = @base,
                        Rates = Enumerable.Empty<ExchangeRateInfo>().ToList()
                    };

                var exchangeInfoList = ZeroFormatterSerializer.Deserialize<CacheExchangeInfo>(exchangeInfoCache);

                return target == ExchangeType.NONE
                    ? exchangeInfoList
                    : exchangeInfoList.FilterRatesByType(target);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task SetCacheExchangeInfo(CacheExchangeInfo exchangeInfo, int expiredInMsec)
        {
            try
            {
                await _distributedCache.SetAsync(exchangeInfo.BaseType.ToString(),
                    ZeroFormatterSerializer.Serialize(exchangeInfo),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMilliseconds(expiredInMsec)
                    });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
    }
}
