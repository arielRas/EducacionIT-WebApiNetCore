using System;
using BookStore.Data.Databases.BookStoreDb.Entities;

namespace BookStore.Data.Repository.Interfaces;

public interface IGenreRepository
{
    Task<Genre> GetByCodeAsync(string code);
    Task<Genre> GetByIdAsync(int id);
    Task<IEnumerable<Genre>> GetAllAsync();
    Task CreateAsync(Genre genre);
    Task UpdateAsync(string code, Genre genre);
    Task DeleteAsync(string code);
}
