using System;
using BookStore.Data.Databases.BookStoreDb.Entities;

namespace BookStore.Data.Repository.Interfaces;

public interface IIsbnRepository
{
    Task<Isbn> GetByCodeAsync(string code);
    Task<Isbn> GetByEditionIdAsync(Guid id);
    Task CreateAsync(Isbn isbn);
    Task UpdateAsync(string code, string newCode);
    Task UpdateByEditionIdAsync(Guid id, string newCode);
    Task DeleteAsync(string code);
}
