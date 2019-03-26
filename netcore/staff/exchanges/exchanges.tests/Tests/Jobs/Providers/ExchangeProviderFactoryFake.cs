using exchanges.server.Infrastructure.Providers.Interfaces;
using System;

namespace exchanges.tests.Tests.Jobs.Providers
{
    class ExchangeProviderFactoryFake : IExchangeProviderFactory
    {
        public bool ThrowException { get; set; }

        public IExchangeProvider GetProvider(string providerName)
        {
            if (ThrowException)
                throw new ArgumentNullException();

            return new LocalExchangeProviderFake();
        }
    }
}
