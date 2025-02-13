using System;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using BookStore.Common.Exceptions;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repository.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly BookStoreDbContext _context;
    private readonly DbSet<Genre> _dbSet;
    public GenreRepository(BookStoreDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Genre>();
    }
     
    public async Task<Genre> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id)
                          ?? throw new ResourceNotFoundException($"The genre with ID: {id} has not been found");
    }

    public async Task<Genre> GetByCodeAsync(string code)
    {
        return await _dbSet.FirstOrDefaultAsync(g => g.Code == code)
                          ?? throw new ResourceNotFoundException($"The genre with code: {code} has not been found");
    }

    public async Task<IEnumerable<Genre>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task CreateAsync(Genre genre)
    {
        await _dbSet.AddAsync(genre);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(string code, Genre genre)
    {
        var existingEntity = await _dbSet.FirstOrDefaultAsync(g => g.Code == code)
            ?? throw new ResourceNotFoundException($"The genre with code: {code} has not been found");
        
        _context.Entry(existingEntity).CurrentValues.SetValues(genre);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string code)
    {
        var existingEntity = await _dbSet.FirstOrDefaultAsync(g => g.Code == code)
            ?? throw new ResourceNotFoundException($"The genre with code: {code} has not been found");

        _dbSet.Remove(existingEntity);

        await _context.SaveChangesAsync();
    }    
}
