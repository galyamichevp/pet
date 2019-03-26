using exchanges.server.Entities;
using exchanges.server.Infrastructure.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace exchanges.tests.Tests.Jobs.Repositories
{
    internal class RedisRepositoryFake : IRedisRepository
    {
        public async Task<CacheExchangeInfo> GetExchangeInfo(ExchangeType @base, ExchangeType target)
        {
            throw new NotImplementedException();
        }

        public async Task SetCacheExchangeInfo(CacheExchangeInfo exchangeInfo, int expiredInMsec)
        {
        }
    }
}
