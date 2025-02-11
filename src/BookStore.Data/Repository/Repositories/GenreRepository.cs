using System;
using BookStore.Common.Exceptions;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repository.Repositories;

public class GenreRepository : Repository<Genre>, IGenreRepository
{
    public GenreRepository(BookStoreDbContext context) : base(context){}

    public async Task<Genre> GetByCodeAsync(string code)
    {
        return await dbSet.FirstOrDefaultAsync(g => g.Code == code)
                          ?? throw new ResourceNotFoundException($"The resource with code: {code} has not been found");
    }
}
