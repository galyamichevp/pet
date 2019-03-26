using exchanges.server.Entities;
using exchanges.server.Entities.OER;
using exchanges.server.Infrastructure.Providers.Interfaces;
using exchanges.server.Resources;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;

namespace exchanges.server.Infrastructure.Providers
{
    internal class OERExchangeProvider : IOERExchangeProvider
    {
        public int ExpiredInMsec { get; set; }

        public string Host { get; set; }

        public string ProviderName => Constants.Providers.OERProvider;

        private readonly ILogger<OERExchangeProvider> _logger;

        public OERExchangeProvider(ILogger<OERExchangeProvider> logger)
        {
            _logger = logger;
        }

        public ExchangeInfo GetExchangeInfo(ExchangeType baseType)
        {
            try
            {
                var client = new RestClient(Host);
                var request = new RestRequest(Method.GET);
                var response = client.Execute<OERResponse>(request);

                return response.Data.ToExchangeInfo();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, ex.Message);
                throw ex;
            }
        }
    }
}
