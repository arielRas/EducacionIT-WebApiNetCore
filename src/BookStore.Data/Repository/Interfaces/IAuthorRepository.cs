using System;
using BookStore.Data.Databases.BookStoreDb.Entities;

namespace BookStore.Data.Repository.Interfaces;

public interface IAuthorRepository
{
    Task<Author> GetByIdAsync(int id);
    Task<Author> GetByIdWithRelationshipsAsync(int id);
    Task<IEnumerable<Author>> GetAllAsync();
    Task<IEnumerable<Author>> GetAllFilteredAsync(string filter);
    Task CreateAsync(Author author);
    Task UpdateAsync(int id, Author author);
    Task DeleteAsync(int id);
}