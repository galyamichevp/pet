using exchanges.server.Configurations.Entities;
using exchanges.server.Entities;
using exchanges.server.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace exchanges.server.Infrastructure.Repositories
{
    internal class MongoRepository : IMongoRepository
    {
        private readonly MongoConfig _mongoConfig;
        private readonly IMongoCollection<StoreExchangeInfo> _exchangeInfos;
        private readonly ILogger<MongoRepository> _logger;

        public MongoRepository(IOptions<MongoConfig> mongoConfig, ILogger<MongoRepository> logger)
        {
            _mongoConfig = mongoConfig.Value;
            _logger = logger;

            var client = new MongoClient(_mongoConfig.Host);
            var database = client.GetDatabase(_mongoConfig.DBName);
            _exchangeInfos = database.GetCollection<StoreExchangeInfo>(_mongoConfig.DBCollection);
        }

        public async Task SaveExchangeInfo(StoreExchangeInfo exchangeInfo)
        {
            await _exchangeInfos.InsertOneAsync(exchangeInfo);
        }
    }
}
