using System;
using BookStore.Data.Databases.BookStoreDb.Entities;

namespace BookStore.Data.Repository.Interfaces;

public interface IGenreRepository : IRepository<Genre>
{
    Task<Genre> GetByCodeAsync(string code);
}
