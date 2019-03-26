namespace exchanges.server.Infrastructure.Providers.Interfaces
{
    public interface IExchangeProviderFactory
    {
        IExchangeProvider GetProvider(string providerName);
    }
}