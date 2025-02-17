using System;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Repository.Interfaces;
using BookStore.Data.Repository.Repositories;
using BookStore.Data.UnitOfWork.Interfaces;

namespace BookStore.Data.UnitOfWork.Implementation;

public class EditionUnitOfWork : IEditionUnitOfWork
{
    private readonly BookStoreDbContext _context;
    private IEditionRepository? _editionRepository;
    private IEditionPriceRepository? _editionPriceRepository;
    private IIsbnRepository? _isbnRepository;

    public EditionUnitOfWork(BookStoreDbContext context)
        => _context = context;

    public IEditionRepository EditionRepository 
        => _editionRepository ??= new EditionRepository(_context);

    public IEditionPriceRepository EditionPriceRepository 
        => _editionPriceRepository ??= new EditionPriceRepository(_context);

    public IIsbnRepository IsbnRepository 
        => _isbnRepository ??= new IsbnRepository(_context);

    public void Dispose()
        => _context?.Dispose();

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();        
}