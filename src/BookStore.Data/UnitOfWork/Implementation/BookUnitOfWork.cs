using System;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Repository.Interfaces;
using BookStore.Data.Repository.Repositories;
using BookStore.Data.UnitOfWork.Interfaces;

namespace BookStore.Data.UnitOfWork.Implementation;

public class BookUnitOfWork : IBookUnitOfWork
{
    private  readonly BookStoreDbContext _context;
    private  IBookRepository? _bookRepository;
    private  IGenreRepository? _genreRepository;
    private  IAuthorRepository? _authorRepository;

    public BookUnitOfWork(BookStoreDbContext context)
        => _context = context;

    public IBookRepository BookRepository 
        => _bookRepository ??= new BookRepository(_context);

    public IGenreRepository GenreRepository 
        => _genreRepository ??= new GenreRepository(_context);

    public IAuthorRepository AuthorRepository 
        => _authorRepository ??= new AuthorRepository(_context);

    public void Dispose()
        => _context.Dispose();

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}