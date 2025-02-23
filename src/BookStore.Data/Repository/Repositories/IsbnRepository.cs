using System;
using BookStore.Common.Exceptions;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repository.Repositories;

public class IsbnRepository : IIsbnRepository
{
    private readonly BookStoreDbContext _context;
    private readonly DbSet<Isbn> _dbSet;

    public IsbnRepository(BookStoreDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Isbn>();
    }
    public async Task<Isbn> GetByCodeAsync(string code)
    {
        return await _dbSet.FindAsync(code) 
            ?? throw new ResourceNotFoundException($"The edition with ISBN {code} does not exist");
    }

    public async Task<Isbn> GetByEditionIdAsync(Guid id)
    {
        var isbn = await _dbSet.FirstAsync(i => i.EditionId == id)
            ?? throw new ResourceNotFoundException($"There is no ISBN for edition {id}");
        
        return isbn;
    }

    public async Task CreateAsync(Isbn isbn)
        => await _dbSet.AddAsync(isbn);


    public async Task UpdateAsync(string code, string newCode)
    {
        var isbn = await _dbSet.FindAsync(code) 
            ?? throw new ResourceNotFoundException($"The edition with ISBN {code} does not exist");
        
        isbn.Code = newCode;

        await _context.SaveChangesAsync();
    }

    public async Task UpdateByEditionIdAsync(Guid id, string newCode)
    {
        var isbn = await _dbSet.FirstAsync(i => i.EditionId == id)
            ?? throw new ResourceNotFoundException($"There is no ISBN for edition {id}");
        
        isbn.Code = newCode;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string code)
    {
        var existingEntity = await _dbSet.FindAsync(code) 
            ?? throw new ResourceNotFoundException($"The edition with ISBN {code} does not exist");

        _dbSet.Remove(existingEntity);
    }      
}
