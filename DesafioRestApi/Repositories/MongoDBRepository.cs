using DesafioRestApi.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DesafioRestApi.Repositories
{
    public class MongoDBRepository : IMongoDBRepository
    {
        public readonly IMongoClient client;
        public readonly IMongoDatabase database;
        public readonly IMongoCollection<Book> collection;
        private readonly IConfiguration _config;

        public MongoDBRepository(IConfiguration config)
        {
            _config = config;
            client = new MongoClient(_config["ConnectionStrings:ConnectionStringUri"]);
            database = client.GetDatabase(_config["ConnectionStrings:Database"]);
            collection = database.GetCollection<Book>(_config["ConnectionStrings:Collection"]);
        }

        public IMongoCollection<Book> GetCollection()
        {
            return collection; 
        }
    }
}
