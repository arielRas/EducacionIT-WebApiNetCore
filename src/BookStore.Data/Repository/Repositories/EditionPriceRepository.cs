using System;
using BookStore.Common.Exceptions;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repository.Repositories;

public class EditionPriceRepository : IEditionPriceRepository
{
    private readonly BookStoreDbContext _context;
    private readonly DbSet<EditionPrice> _dbSet;

    public EditionPriceRepository(BookStoreDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<EditionPrice>();
    }

    public async Task<EditionPrice> GetByIdAsync(Guid id)
    {
         return await _dbSet.FindAsync(id) 
            ?? throw new ResourceNotFoundException($"The Edition price with ID: {id} has not been found");
    }

    public async Task CreateAsync(EditionPrice editionPrice)
        => await _dbSet.AddAsync(editionPrice);

    public async Task DeleteAsync(Guid id)
    {
        var existingEntity = await _dbSet.FindAsync(id) 
            ?? throw new ResourceNotFoundException($"The Edition price with ID: {id} has not been found");

        _dbSet.Remove(existingEntity);
    }    

    public async Task UpdateAsync(Guid id, decimal price)
    {
        var existingEntity = await _dbSet.FindAsync(id) 
            ?? throw new ResourceNotFoundException($"The Edition price with ID: {id} has not been found");

        existingEntity.Price = price;

        await _context.SaveChangesAsync();
    }
}
