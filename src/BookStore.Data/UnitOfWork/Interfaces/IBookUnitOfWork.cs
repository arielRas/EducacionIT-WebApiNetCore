using System;
using BookStore.Data.Repository.Interfaces;

namespace BookStore.Data.UnitOfWork.Interfaces;

public interface IBookUnitOfWork : IUnitOfWork
{
    IBookRepository BookRepository {get;}
    IGenreRepository GenreRepository {get;}
    IAuthorRepository AuthorRepository {get;}
}
