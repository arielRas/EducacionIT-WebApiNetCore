using System;
using System.Runtime.InteropServices;
using BookStore.Data.Databases.BookStoreDb.Entities;

namespace BookStore.Data.Repository.Interfaces;

public interface IEditionPriceRepository
{
    Task<EditionPrice>GetByIdAsync(Guid id);
    Task CreateAsync(EditionPrice editionPrice);
    Task UpdateAsync(Guid id, decimal price);
    Task DeleteAsync(Guid id);
}
