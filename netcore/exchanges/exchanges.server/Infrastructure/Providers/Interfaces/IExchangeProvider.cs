using exchanges.server.Entities;

namespace exchanges.server.Infrastructure.Providers.Interfaces
{
    public interface IExchangeProvider
    {
        int ExpiredInMsec { get; set; }

        string ProviderName { get; }

        ExchangeInfo GetExchangeInfo(ExchangeType baseType);
    }
}