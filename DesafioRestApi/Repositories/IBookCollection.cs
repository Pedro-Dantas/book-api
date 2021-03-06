﻿using DesafioRestApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioRestApi.Repositories
{
    public interface IBookCollection
    {
        Task InsertBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(string id);
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBookById(string id);
    }
}
