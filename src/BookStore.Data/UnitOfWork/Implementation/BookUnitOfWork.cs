using System;
using System.Transactions;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Repository.Interfaces;
using BookStore.Data.Repository.Repositories;
using BookStore.Data.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookStore.Data.UnitOfWork.Implementation;

public class BookUnitOfWork : IBookUnitOfWork
{
    private readonly BookStoreDbContext _context;
    private IBookRepository? _bookRepository;
    private IGenreRepository? _genreRepository;
    private IAuthorRepository? _authorRepository;
    private IDbContextTransaction? _transaction;

    public BookUnitOfWork(BookStoreDbContext context)
        => _context = context;

    public IBookRepository BookRepository 
        => _bookRepository ??= new BookRepository(_context);

    public IGenreRepository GenreRepository 
        => _genreRepository ??= new GenreRepository(_context);

    public IAuthorRepository AuthorRepository 
        => _authorRepository ??= new AuthorRepository(_context);

    public async Task BeginTransactionAsync()
        => _transaction ??= await _context.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync()
    {
        if(IsTransactionActive())
        {
            await _context.SaveChangesAsync();
            await _transaction!.CommitAsync();           
        }            
    }

    public async Task RollbackTransactionAsync()
    {
        if(IsTransactionActive())
        {
            await _transaction!.RollbackAsync();
            _transaction?.Dispose();
            _transaction = null;
        }            
    }

    private bool IsTransactionActive()
        => _transaction is not null && _transaction.GetDbTransaction().Connection is not null;
}