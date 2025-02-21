using System;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Repository.Interfaces;
using BookStore.Data.Repository.Repositories;
using BookStore.Data.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookStore.Data.UnitOfWork.Implementation;

public class EditionUnitOfWork : IEditionUnitOfWork
{
    private readonly BookStoreDbContext _context;
    private IEditionRepository? _editionRepository;
    private IEditionPriceRepository? _editionPriceRepository;
    private IIsbnRepository? _isbnRepository;
    private IDbContextTransaction? _transaction;

    public EditionUnitOfWork(BookStoreDbContext context)
        => _context = context;

    public IEditionRepository EditionRepository 
        => _editionRepository ??= new EditionRepository(_context);

    public IEditionPriceRepository EditionPriceRepository 
        => _editionPriceRepository ??= new EditionPriceRepository(_context);

    public IIsbnRepository IsbnRepository 
        => _isbnRepository ??= new IsbnRepository(_context);

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