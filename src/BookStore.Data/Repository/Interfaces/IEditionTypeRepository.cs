using System;
using BookStore.Data.Databases.BookStoreDb.Entities;

namespace BookStore.Data.Repository.Interfaces;

public interface IEditionTypeRepository : IRepository<EditionType>
{
    Task<EditionType> GetByCodeAsync(string code);
}
