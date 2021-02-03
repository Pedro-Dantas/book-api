using DesafioRestApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DesafioRestApi.Repositories
{
    public class BookCollection : IBookCollection
    {
        private readonly IMongoDBRepository _repository; 

        public BookCollection(IMongoDBRepository mongoDBRepository)
        {
            _repository = mongoDBRepository;
        }

        public async Task DeleteBook(string id)
        {
            var filter = Builders<Book>.Filter.Eq(x => x.Id, id);
            await _repository.GetCollection().DeleteOneAsync(filter);
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _repository.GetCollection().FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Book> GetBookById(string id)
        {
            var filter = Builders<Book>.Filter.Eq(x => x.Id, id);

            return await _repository.GetCollection().FindAsync(filter).Result.FirstAsync();
        }

        public async Task InsertBook(Book book)
        {
            await _repository.GetCollection().InsertOneAsync(book);
        }

        public async Task UpdateBook(Book book)
        {
            var filter = Builders<Book>.Filter.Eq(x => x.Id, book.Id);

            await _repository.GetCollection().ReplaceOneAsync(filter, book);
        }
    }
}
