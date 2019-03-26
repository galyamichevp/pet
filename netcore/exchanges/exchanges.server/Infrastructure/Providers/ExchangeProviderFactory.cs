using exchanges.server.Configurations.Entities;
using exchanges.server.Infrastructure.Providers.Interfaces;
using exchanges.server.Resources;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace exchanges.server.Infrastructure.Providers
{
    internal sealed class ExchangeProviderFactory : IExchangeProviderFactory
    {
        private readonly List<ProviderConfig> _providerConfigs;
        private readonly IOERExchangeProvider _oerExchangeProvider;
        private readonly ILocalExchangeProvider _localExchangeProvider;

        public ExchangeProviderFactory(IOptions<List<ProviderConfig>> providerConfigs,
            IOERExchangeProvider oerExchangeProvider,
            ILocalExchangeProvider localExchangeProvider)
        {
            _providerConfigs = providerConfigs.Value;
            _oerExchangeProvider = oerExchangeProvider;
            _localExchangeProvider = localExchangeProvider;
        }

        public IExchangeProvider GetProvider(string providerName)
        {
            var providerConfig = _providerConfigs
                .FirstOrDefault(pc => pc.Name == providerName && pc.IsEnabled);

            switch (providerConfig.Name)
            {
                case Constants.Providers.LocalProvider:
                    _localExchangeProvider.ExpiredInMsec = providerConfig.ExpiredInMsec;
                    return _localExchangeProvider;
                case Constants.Providers.OERProvider:
                    _oerExchangeProvider.Host = providerConfig.Host;
                    _oerExchangeProvider.ExpiredInMsec = providerConfig.ExpiredInMsec;
                    return _oerExchangeProvider;
                default:
                    return null;
            }
        }
    }
}