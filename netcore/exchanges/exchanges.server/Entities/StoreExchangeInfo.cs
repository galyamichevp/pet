using System;

namespace exchanges.server.Entities
{
    public class StoreExchangeInfo
    {
        public string ProviderName { get; protected set; }

        public DateTime Created { get; protected set; } = DateTime.Now;

        public dynamic ExchangeInfo { get; protected set; }

        public StoreExchangeInfo(string providerName, dynamic exchangeInfo)
        {
            if (string.IsNullOrEmpty(providerName))
                throw new ArgumentNullException(nameof(providerName));

            if (exchangeInfo == null)
                throw new ArgumentNullException(nameof(exchangeInfo));

            ProviderName = providerName;
            ExchangeInfo = exchangeInfo;
        }
    }
}