using System;
using System.Linq.Expressions;
using BookStore.Common.Exceptions;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repository.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookStoreDbContext _context;
    private readonly DbSet<Book> _dbset;

    public BookRepository(BookStoreDbContext context)
    {
        _context = context;
        _dbset = _context.Set<Book>();
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        return await _dbset.FindAsync(id) 
            ?? throw new ResourceNotFoundException($"The book with ID: {id} has not been found");
    }

    public async Task<Book> GetByIdWithRelationshipsAsync(int id)
    {
        return await _dbset.Include(b => b.Authors)
                           .Include(b => b.Genres)
                           .Include(b => b.Editions)                           
                                .ThenInclude(e => e.EditionType)
                           .FirstOrDefaultAsync(b => b.BookId == id)
                           ?? throw new ResourceNotFoundException($"The book with ID: {id} has not been found");
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        var books = await _dbset.ToListAsync();

        if(!books.Any()) 
            throw new ResourceNotFoundException($"There are no books to display");

        return books;
    }

    public async Task<IEnumerable<Book>> GetAllFilteredByTitleOrAuthorAsync(string filter)
    {
        IQueryable<Book> query = _dbset.AsQueryable();

        query = query.Where(b =>
                        b.Title.ToLower().Contains(filter)||         
                        b.Authors.Any(a => 
                            a.Name.ToLower().Contains(filter) || 
                            (a.LastName != null && a.LastName.Contains(filter))
                        )   
                    );                     

        var books = await query.ToListAsync();

        if(!books.Any()) 
            throw new ResourceNotFoundException($"There are no books to display");

        return books;
    }

    public async Task<IEnumerable<Book>> GetAllFilteredAsync(Expression<Func<Book, bool>> filter)
    {
        var books = await _dbset.Where(filter).ToListAsync();

        if(!books.Any()) 
            throw new ResourceNotFoundException($"There are no books to display");

        return books;
    }    

    public async Task CreateAsync(Book book)
    {
        await _dbset.AddAsync(book);
        await _context.SaveChangesAsync();       
    }

    public async Task UpdateAsync(int id, Book book)
    {
        var existingEntity = await _dbset.FindAsync(id)
            ?? throw new ResourceNotFoundException($"The book with ID: {id} has not been found");
        
        _context.Entry(existingEntity).CurrentValues.SetValues(book);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existingEntity = await _dbset.FindAsync(id)
            ?? throw new ResourceNotFoundException($"The book with ID: {id} has not been found");

        _dbset.Remove(existingEntity);

        await _context.SaveChangesAsync();
    }    
}
