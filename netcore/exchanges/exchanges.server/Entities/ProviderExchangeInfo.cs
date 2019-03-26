using System;
using ZeroFormatter;

namespace exchanges.server.Entities
{
    [ZeroFormattable]
    public class ProviderExchangeInfo : ExchangeInfo
    {
        [Index(3)]
        public virtual string ProviderName { get; protected set; }

        public ProviderExchangeInfo()
            : base()
        {
        }

        public ProviderExchangeInfo(string providerName)
            : this()
        {
            if (string.IsNullOrEmpty(providerName))
                throw new ArgumentNullException(nameof(providerName));
                
            ProviderName = providerName;
        }
    }
}