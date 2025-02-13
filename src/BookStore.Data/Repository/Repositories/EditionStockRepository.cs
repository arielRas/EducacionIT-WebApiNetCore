using System;
using BookStore.Common.Exceptions;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repository.Repositories;

public class EditionStockRepository : IEditionStockRepository
{
    private readonly BookStoreDbContext _context;
    private readonly DbSet<EditionStock> _dbSet;

    public EditionStockRepository(BookStoreDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<EditionStock>();
    }

    public async Task<EditionStock> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id) 
            ?? throw new ResourceNotFoundException($"The Edition stock with ID: {id} has not been found");
    }

    public async Task CreateAsync(EditionStock editionStock)
        => await _dbSet.AddAsync(editionStock);

    public async Task DeleteAsync(Guid id)
    {
        var existingEntity = await _dbSet.FindAsync(id) 
            ?? throw new ResourceNotFoundException($"The Edition stock with ID: {id} has not been found");

        _dbSet.Remove(existingEntity);
    }    

    public async Task UpdateAsync(Guid id, int stock)
    {
        var existingEntity = await _dbSet.FindAsync(id) 
            ?? throw new ResourceNotFoundException($"The Edition stock with ID: {id} has not been found");

        existingEntity.Stock = stock;

        await _context.SaveChangesAsync();
    }
}
