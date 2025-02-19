using System;
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

    public async Task<Genre> GetByCodeAsync(string code)
    {
        return await _dbSet.FindAsync(code)
            ?? throw new ResourceNotFoundException($"The genre with code: {code} has not been found");
    }

    public async Task<IEnumerable<Genre>> GetAllAsync()
        => await _dbSet.ToListAsync();


    public async Task<IEnumerable<Genre>> GetAllFilteredByCodeAsync(IEnumerable<string> codeList)
    {
        var query = _dbSet.AsQueryable();

        query = query.AsNoTracking().Where(g => codeList.Contains(g.Code));

        var genres = await query.ToListAsync();

        if(genres.Count() != codeList.Count())
            throw new ResourceNotFoundException();
        
        return genres;
    }


    public async Task CreateAsync(Genre genre)
    {
        await _dbSet.AddAsync(genre);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(string code, Genre genre)
    {
        var existingEntity = await _dbSet.FirstOrDefaultAsync(g => g.Code == code)
            ?? throw new ResourceNotFoundException($"The genre with code: {code} has not been found");
        
        existingEntity.Name = genre.Name;

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
