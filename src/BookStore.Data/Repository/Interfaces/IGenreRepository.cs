using System;
using BookStore.Data.Databases.BookStoreDb.Entities;

namespace BookStore.Data.Repository.Interfaces;

public interface IGenreRepository
{
    Task<Genre> GetByCodeAsync(string code);
    Task<IEnumerable<Genre>> GetAllAsync();
    Task<IEnumerable<Genre>> GetByCodesAsync(IEnumerable<string> codeList);
    Task CreateAsync(Genre genre);
    Task UpdateAsync(string code, Genre genre);
    Task DeleteAsync(string code);
}
