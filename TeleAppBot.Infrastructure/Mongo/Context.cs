using MongoDB.Driver;
using TeleAppBot.Infrastructure.Mongo.Models;

namespace TeleAppBot.Infrastructure.Mongo
{
    public class Context
    {

        private readonly IMongoDatabase _mongoDatabase;

        public Context(string connectionString)
        {
            var mongoConnectionUrl = new MongoUrl(connectionString);
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);
            var mongoClient = new MongoClient(mongoClientSettings);

            _mongoDatabase = mongoClient.GetDatabase(mongoConnectionUrl.DatabaseName);
        }

        public IMongoCollection<MensagemModel> Canais => _mongoDatabase.GetCollection<MensagemModel>("Mensagens");
    }
}
