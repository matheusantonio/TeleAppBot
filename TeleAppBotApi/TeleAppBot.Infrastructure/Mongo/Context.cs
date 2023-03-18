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

        public IMongoCollection<MensagemModel> Mensagens => _mongoDatabase.GetCollection<MensagemModel>("Mensagens");
        public IMongoCollection<ConversaModel> Conversas => _mongoDatabase.GetCollection<ConversaModel>("Conversas");
        public IMongoCollection<ContatoModel> Contatos => _mongoDatabase.GetCollection<ContatoModel>("Contatos");
    }
}
