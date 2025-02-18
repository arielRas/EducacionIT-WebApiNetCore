using System;
using BookStore.Common.Exceptions;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repository.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly BookStoreDbContext _context;

    private readonly DbSet<Author> _dbSet;

    public AuthorRepository(BookStoreDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Author>();
    }

    public async Task<Author> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id) 
            ?? throw new ResourceNotFoundException($"The author with ID: {id} has not been found");
    }

    public async Task<Author> GetByIdWithRelationshipsAsync(int id)
    {
        return await _dbSet.Include(a => a.Books)
                           .FirstOrDefaultAsync(a => a.AuthorId == id)
                           ?? throw new ResourceNotFoundException($"The author with ID: {id} has not been found");
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        var authors = await _dbSet.ToListAsync();

        if(!authors.Any()) 
            throw new ResourceNotFoundException($"There are no authors to display");

        return authors;
    }

    public async Task<IEnumerable<Author>> GetAllFilteredByIdAsync(IEnumerable<int> idList)
    {
        var authors = await _dbSet.Where(a => idList.Contains(a.AuthorId)).ToListAsync();

        if(!authors.Any()) 
            throw new ResourceNotFoundException();

        return authors;
    }

    public async Task<IEnumerable<Author>> GetAllFilteredAsync(string filter)
    {
        var query = _dbSet.AsQueryable();

        query = query.Where(a => a.Name.Contains(filter) || (a.LastName != null && a.LastName.Contains(filter)));

        var authors = await query.ToListAsync();

        if(!authors.Any()) throw new ResourceNotFoundException($"There are no authors to display");

        return authors;
    }

    public async Task CreateAsync(Author author)
    {
        await _dbSet.AddAsync(author);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Author author)
    {
        var existingEntity = await _dbSet.FindAsync(id)
            ?? throw new ResourceNotFoundException($"The author with ID: {id} has not been found");

        _context.Entry(existingEntity).CurrentValues.SetValues(author);

        await SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existingEntity = await _dbSet.FindAsync(id)
            ?? throw new ResourceNotFoundException($"The author with ID: {id} has not been found");

        _dbSet.Remove(existingEntity);

        await SaveChangesAsync();
    }   

    private async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}
