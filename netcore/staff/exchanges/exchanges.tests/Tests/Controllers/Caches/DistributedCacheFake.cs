using exchanges.server.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZeroFormatter;

namespace exchanges.tests.Tests.Controllers.Repositories
{
    internal class DistributedCacheFake : IDistributedCache
    {
        public byte[] Get(string key)
        {
            switch (key)
            {
                case "USD":
                    return ZeroFormatterSerializer.Serialize(new CacheExchangeInfo(1000)
                    {
                        BaseType = ExchangeType.USD,
                        Published = DateTimeOffset.Now,
                        Rates = new List<ExchangeRateInfo>
                        {
                            new ExchangeRateInfo
                            {
                                Rate=1,
                                Type = ExchangeType.EUR
                            },
                            new ExchangeRateInfo
                            {
                                Rate=1,
                                Type = ExchangeType.RUB
                            },
                            new ExchangeRateInfo
                            {
                                Rate=1,
                                Type = ExchangeType.USD
                            }
                        }
                    });
                case "NONE":
                    return ZeroFormatterSerializer.Serialize(new CacheExchangeInfo(1000)
                    {
                        BaseType = ExchangeType.USD,
                        Published = DateTimeOffset.Now,
                        Rates = new List<ExchangeRateInfo>()
                    });
                default:
                    throw new ArgumentNullException();
            }
        }

        public async Task<byte[]> GetAsync(string key, CancellationToken token = default(CancellationToken))
        {
            switch (key)
            {
                case "USD":
                    return ZeroFormatterSerializer.Serialize(new CacheExchangeInfo(1000)
                    {
                        BaseType = ExchangeType.USD,
                        Published = DateTimeOffset.Now,
                        Rates = new List<ExchangeRateInfo>
                        {
                            new ExchangeRateInfo
                            {
                                Rate=1,
                                Type = ExchangeType.EUR
                            },
                            new ExchangeRateInfo
                            {
                                Rate=1,
                                Type = ExchangeType.RUB
                            },
                            new ExchangeRateInfo
                            {
                                Rate=1,
                                Type = ExchangeType.USD
                            }
                        }
                    });
                case "NONE":
                    return ZeroFormatterSerializer.Serialize(new CacheExchangeInfo(1000)
                    {
                        BaseType = ExchangeType.USD,
                        Published = DateTimeOffset.Now,
                        Rates = new List<ExchangeRateInfo>()
                    });
                default:
                    throw new ArgumentNullException();
            }
        }

        public void Refresh(string key)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(string key, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
