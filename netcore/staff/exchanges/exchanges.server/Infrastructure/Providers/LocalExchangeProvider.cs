using exchanges.server.Entities;
using exchanges.server.Infrastructure.Providers.Interfaces;
using exchanges.server.Resources;
using System;
using System.Linq;

namespace exchanges.server.Infrastructure.Providers
{
    internal class LocalExchangeProvider : ILocalExchangeProvider
    {
        public int ExpiredInMsec { get; set; }

        public string ProviderName => Constants.Providers.LocalProvider;

        public ExchangeInfo GetExchangeInfo(ExchangeType baseType)
        {
            return new ExchangeInfo()
            {
                BaseType = baseType,
                Published = DateTime.Now,
                Rates = ExchangeExtensions.GetExchangeTypes()
                    .Select(et => new ExchangeRateInfo
                    {
                        Type = et,
                        Rate = new Random().Next(1, 100)
                    })
                    .ToList()
            };
        }
    }
}
