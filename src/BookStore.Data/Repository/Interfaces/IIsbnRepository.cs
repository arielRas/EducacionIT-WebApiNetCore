using System;
using BookStore.Data.Databases.BookStoreDb.Entities;

namespace BookStore.Data.Repository.Interfaces;

public interface IIsbnRepository
{
    Task<Isbn> GetByCodeAsync(string code);
    Task CreateAsync(Isbn isbn);
    Task UpdateAsync(string code, Isbn isbn);
    Task DeleteAsync(string code);
}
