using System;
using BookStore.Common.Exceptions;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repository.Repositories;

public class EditionTypeRepository : IEditionTypeRepository
{
    private readonly BookStoreDbContext _context;
    private readonly DbSet<EditionType> _dbSet;
    public EditionTypeRepository(BookStoreDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<EditionType>();
    }

    public async Task<EditionType> GetByCodeAsync(string code)
    {
        return await _dbSet.FirstOrDefaultAsync(g => g.Code == code)
            ?? throw new ResourceNotFoundException($"The edition type with code: {code} has not been found");
    }

    public async Task<EditionType> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id)
            ?? throw new ResourceNotFoundException($"The edition type with ID: {id} has not been found");
    }

    public async Task<IEnumerable<EditionType>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task CreateAsync(EditionType editionType)
    {
        await _dbSet.AddAsync(editionType);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(string code, EditionType editionType)
    {
        var existingEntity = await _dbSet.FirstOrDefaultAsync(e => e.Code == code)
            ?? throw new ResourceNotFoundException($"The edition type with code: {code} has not been found");
        
        existingEntity.Name = editionType.Name;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string code)
    {
        var existingEntity = await _dbSet.FirstOrDefaultAsync(e => e.Code == code)
            ?? throw new ResourceNotFoundException($"The edition type with code: {code} has not been found");

        _dbSet.Remove(existingEntity);

        await _context.SaveChangesAsync();
    }    
}
