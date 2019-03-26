using exchanges.server.Entities;
using System.Threading.Tasks;

namespace exchanges.server.Infrastructure.Repositories.Interfaces
{
    internal interface IMongoRepository
    {
        Task SaveExchangeInfo(StoreExchangeInfo exchangeInfo);
    }
}
