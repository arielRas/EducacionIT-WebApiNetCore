using System;
using BookStore.Data.Databases.BookStoreDb.Entities;

namespace BookStore.Data.Repository.Interfaces;

public interface IEditionTypeRepository
{
    Task<EditionType> GetByCodeAsync(string code);
    Task<IEnumerable<EditionType>> GetAllAsync();
    Task CreateAsync(EditionType editionType);
    Task UpdateAsync(string code, EditionType editionType);
    Task DeleteAsync(string code);
}
