using System;
using ZeroFormatter;

namespace exchanges.server.Entities
{
    [ZeroFormattable]
    public class CacheExchangeInfo : ExchangeInfo
    {
        [Index(3)]
        public virtual DateTimeOffset Expired { get; set; }

        public CacheExchangeInfo()
            : base()
        {
        }

        public CacheExchangeInfo(int expiredInMsec)
            : this()
        {
            Expired = DateTimeOffset.Now.AddMilliseconds(expiredInMsec);
        }
    }
}