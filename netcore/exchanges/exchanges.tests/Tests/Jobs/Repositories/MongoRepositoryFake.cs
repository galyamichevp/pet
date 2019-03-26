using exchanges.server.Entities;
using exchanges.server.Infrastructure.Repositories.Interfaces;
using System.Threading.Tasks;

namespace exchanges.tests.Tests.Jobs.Repositories
{
    internal class MongoRepositoryFake : IMongoRepository
    {
        public Task SaveExchangeInfo(StoreExchangeInfo exchangeInfo)
        {
            return Task.CompletedTask;
        }
    }
}
