using exchanges.server.Entities.Models;
using exchanges.server.Entities.OER;
using System;
using System.Collections.Generic;
using System.Linq;

namespace exchanges.server.Entities
{
    internal static class ExchangeExtensions
    {
        public static RatesResponse ToRatesResponse(this CacheExchangeInfo exchangeInfo)
        {
            return new RatesResponse
            {
                From = exchangeInfo.BaseType.ToString(),
                Rates = exchangeInfo.Rates
                    .Select(ei => new RateInfo
                    {
                        ExpireAt = exchangeInfo.Expired.DateTime,
                        Rate = ei.Rate,
                        To = ei.Type.ToString()
                    }).ToArray()
            };
        }

        public static ExchangeInfo ToExchangeInfo(this OERResponse oerResponse)
        {
            return new ExchangeInfo
            {
                BaseType = oerResponse.Base,
                Published = DateTimeOffset.FromUnixTimeSeconds(oerResponse.Timestamp),
                Rates = oerResponse.Rates
                    .Where(r => Enum.TryParse<ExchangeType>(r.Key.ToUpperInvariant(), out _))
                    .Select(r => new ExchangeRateInfo { Type = Enum.Parse<ExchangeType>(r.Key.ToUpperInvariant()), Rate = r.Value })
                    .ToList()
            };
        }

        public static StoreExchangeInfo ToStoreExchangeInfo(this ExchangeInfo exchangeInfo, string providerName)
        {
            return new StoreExchangeInfo(providerName, exchangeInfo);
        }

        public static ProviderExchangeInfo ToProviderExchangeInfo(this ExchangeInfo exchangeInfo, string providerName)
        {
            return new ProviderExchangeInfo(providerName)
            {
                BaseType = exchangeInfo.BaseType,
                Published = exchangeInfo.Published,
                Rates = exchangeInfo.Rates.ToList()
            };
        }

        public static CacheExchangeInfo ToCacheExchangeInfo(this ExchangeInfo exchangeInfo, int expiredInMsec)
        {
            return new CacheExchangeInfo(expiredInMsec)
            {
                BaseType = exchangeInfo.BaseType,
                Published = exchangeInfo.Published,
                Rates = exchangeInfo.Rates.ToList()
            };
        }

        public static CacheExchangeInfo FilterRatesByType(this CacheExchangeInfo exchangeInfo, ExchangeType type)
        {
            return new CacheExchangeInfo
            {
                BaseType = exchangeInfo.BaseType,
                Published = exchangeInfo.Published,
                Expired = exchangeInfo.Expired,
                Rates = exchangeInfo.Rates
                    .Where(r => r.Type == type)
                    .ToList()
            };
        }

        public static IEnumerable<ExchangeType> GetExchangeTypes()
        {
            return Enum.GetValues(typeof(ExchangeType)).Cast<ExchangeType>().Where(et => et != ExchangeType.NONE);
        }
    }
}
