using System;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repository.Repositories;

public class EditorialRepository : Repository<Editorial>, IEditorialRepository
{
    public EditorialRepository(BookStoreDbContext context) : base(context){}

    public async Task<bool> Exist(int id)
        => await dbSet.AnyAsync(e => e.EditorialId == id);
}
