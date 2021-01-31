using MongoDB.Driver;

namespace DesafioRestApi.Repositories
{
    public class MongoDBRepository
    {
        public const string _uri = "mongodb://localhost:27017";
        public const string _nameDatabase = "Library";
        public readonly IMongoClient client;
        public readonly IMongoDatabase database;

        public MongoDBRepository()
        {
            client = new MongoClient(_uri);
            database = client.GetDatabase(_nameDatabase);
        }
    }
}
