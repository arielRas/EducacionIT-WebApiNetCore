using System;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Repository.Interfaces;
using BookStore.Data.Repository.Repositories;
using BookStore.Data.UnitOfWork.Interfaces;

namespace BookStore.Data.UnitOfWork.Implementation;

public class EditionUnitOfWork : UnitOfWork<BookStoreDbContext>, IEditionUnitOfWork
{    
    private IEditionRepository? _editionRepository;
    private IEditionPriceRepository? _editionPriceRepository;
    private IIsbnRepository? _isbnRepository;
    private IBookRepository? _bookRepository;
    private IEditorialRepository? _ditorialRepository;

    public EditionUnitOfWork(BookStoreDbContext context) : base(context){}

    public IEditionRepository EditionRepository 
        => _editionRepository ??= new EditionRepository(_context);

    public IEditionPriceRepository EditionPriceRepository 
        => _editionPriceRepository ??= new EditionPriceRepository(_context);

    public IIsbnRepository IsbnRepository 
        => _isbnRepository ??= new IsbnRepository(_context);

    public IBookRepository BookRepository
        => _bookRepository ??= new BookRepository(_context);

    public IEditorialRepository EditorialRepository
        => _ditorialRepository ??= new EditorialRepository(_context);
  
}