using System;
using System.Linq.Expressions;
using BookStore.Data.Databases.BookStoreDb.Entities;

namespace BookStore.Data.Repository.Interfaces;

public interface IBookRepository
{
    Task<Book> GetByIdAsync(int id);
    Task<IEnumerable<Book>> GetAllAsync();
    Task<IEnumerable<Book>> GetAllFilteredAsync(Expression<Func<Book, bool>> filter);
    Task<IEnumerable<Book>> GetFilteredByTitleOrAuthorAsync(string filter);
    Task CreateAsync(Book book);
    Task UpdateAsync(int id, Book book);
    Task UpdateGenresAsync(int bookId, IEnumerable<Genre> genres);
    Task UpdateAuthorsAsync(int bookId, IEnumerable<Author> authors);
    Task DeleteAsync(int id);
    Task<bool> Exist(int id);
}
