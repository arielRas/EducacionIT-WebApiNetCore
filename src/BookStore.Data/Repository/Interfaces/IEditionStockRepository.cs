using System;
using BookStore.Data.Databases.BookStoreDb.Entities;

namespace BookStore.Data.Repository.Interfaces;

public interface IEditionStockRepository
{
    Task<EditionStock>GetByIdAsync(Guid id);
    Task CreateAsync(EditionStock editionStock);
    Task UpdateAsync(Guid id, int stock);
    Task DeleteAsync(Guid id);
}
