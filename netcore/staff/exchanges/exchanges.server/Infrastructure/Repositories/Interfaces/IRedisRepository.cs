using exchanges.server.Entities;
using System.Threading.Tasks;

namespace exchanges.server.Infrastructure.Repositories.Interfaces
{
    public interface IRedisRepository
    {
        Task SetCacheExchangeInfo(CacheExchangeInfo exchangeInfo, int expiredInMsec);
        /// <summary>
        /// Получить информацию о паре курсов валют
        /// </summary>
        /// <param name="base">Продаваемая валюта</param>
        /// <param name="target">Покупаемая валюта</param>
        /// <returns>Объект с информацией о паре курсов валют и временем актуальности</returns>
        Task<CacheExchangeInfo> GetExchangeInfo(ExchangeType @base, ExchangeType target);
    }
}
