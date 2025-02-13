using System;
using System.Linq.Expressions;
using BookStore.Common.Exceptions;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repository.Repositories;

public class EditionRepository : IEditionRepository
{
    private readonly BookStoreDbContext _context;
    private readonly DbSet<Edition> _dbset;

    public EditionRepository(BookStoreDbContext context)
    {
        _context = context;
        _dbset = _context.Set<Edition>();
    }
    
    public async Task<Edition> GetByIdAsync(Guid id)
    {
        return await _dbset.Include(e => e.Isbn)
                           .Include(e => e.Editorial)
                           .Include(e => e.EditionType)
                           .Include(e => e.EditionPrice)
                           .Include(e => e.EditionStock)
                           .FirstOrDefaultAsync(e => e.EditionId == id)
                           ?? throw new ResourceNotFoundException($"The book edition with ID: {id} has not been found");
    }

    public async Task<IEnumerable<Edition>> GetAllAsync()
    {
        var editions = await _dbset.Include(e => e.Isbn)
                                   .Include(e => e.Editorial)
                                   .Include(e => e.EditionType)
                                   .ToListAsync();
        
        if (!editions.Any())
            throw new ResourceNotFoundException($"There are no book editions to display");

        return editions;
    }

    public async Task<IEnumerable<Edition>> GetAllFilteredAsync(Expression<Func<Edition, bool>> filter)
    {
        IQueryable<Edition> query = _dbset.AsQueryable();

        query = query.Include(e => e.Isbn)
                     .Include(e => e.Editorial)
                     .Include(e => e.EditionType)
                     .Where(filter);

        var editions = await query.ToListAsync();

        if (!editions.Any())
            throw new ResourceNotFoundException($"There are no book editions to display");

        return editions;
    }

    public async Task CreateAsync(Edition edition)
    {
        await _dbset.AddAsync(edition);
    }

    public async Task UpdateAsync(Guid id, Edition edition)
    {
        var existingEntity = await _dbset.FindAsync(id)
            ?? throw new ResourceNotFoundException($"The book edition with ID: {id} has not been found");
        
        _context.Entry(existingEntity).CurrentValues.SetValues(edition);

        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(Guid id)
    {
         var existingEntity = await _dbset.FindAsync(id)
            ?? throw new ResourceNotFoundException($"The book edition with ID: {id} has not been found");
        
        _dbset.Remove(existingEntity);
    }    
}
