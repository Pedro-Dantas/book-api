using DesafioRestApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DesafioRestApi.Repositories
{
    public class BookCollection : IBookCollection
    {
        private MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Book> _collection;

        public BookCollection()
        {
            _collection = _repository.database.GetCollection<Book>("Books");
        }

        public async Task DeleteBook(string id)
        {
            var filter = Builders<Book>.Filter.Eq(x => x.Id, id);
            await _collection.DeleteOneAsync(filter);
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Book> GetBookById(string id)
        {
            var filter = Builders<Book>.Filter.Eq(x => x.Id, id);

            return await _collection.FindAsync(filter).Result.FirstAsync();

            //return await _collection.FindAsync(
            //   new BsonDocument{{"_id", id} }).Result.FirstAsync();
        }

        public async Task InsertBook(Book book)
        {
            await _collection.InsertOneAsync(book);
        }

        public async Task UpdateBook(Book book)
        {
            var filter = Builders<Book>.Filter.Eq(x => x.Id, book.Id);

            await _collection.ReplaceOneAsync(filter, book);
        }
    }
}
