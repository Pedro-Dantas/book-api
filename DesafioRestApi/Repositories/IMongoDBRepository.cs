using DesafioRestApi.Model;
using MongoDB.Driver;

namespace DesafioRestApi.Repositories
{
    public interface IMongoDBRepository
    {
        public IMongoCollection<Book> GetCollection();
    }
}
