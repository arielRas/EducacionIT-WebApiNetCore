using System;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Data.Repository.Interfaces;

namespace BookStore.Data.Repository.Repositories;

public class EditorialRepository : Repository<Editorial>, IEditorialRepository
{
    public EditorialRepository(BookStoreDbContext context) : base(context){}
}
