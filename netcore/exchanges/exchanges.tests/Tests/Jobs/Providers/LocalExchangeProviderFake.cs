using exchanges.server.Entities;
using exchanges.server.Infrastructure.Providers.Interfaces;
using exchanges.server.Resources;
using System;
using System.Collections.Generic;

namespace exchanges.tests.Tests.Jobs.Providers
{
    internal class LocalExchangeProviderFake : ILocalExchangeProvider
    {
        public int ExpiredInMsec { get; set; }

        public string ProviderName => Constants.Providers.LocalProvider;

        public ExchangeInfo GetExchangeInfo(ExchangeType baseType)
        {
            return new ExchangeInfo()
            {
                BaseType = baseType,
                Published = DateTime.Now,
                Rates = new List<ExchangeRateInfo>()
            };
        }
    }
}
