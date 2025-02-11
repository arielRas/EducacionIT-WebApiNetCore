using System;
using System.Linq.Expressions;
using BookStore.Common.Exceptions;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repository.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly BookStoreDbContext _context;
    protected DbSet<T> dbSet;    

    public Repository(BookStoreDbContext context)
    {
        _context = context;
        dbSet = context.Set<T>();
    } 

    public async Task<T> GetByIdAsync(Expression<Func<T, bool>> idFilter)
        => await dbSet.Where(idFilter).FirstOrDefaultAsync() ?? throw new ResourceNotFoundException();

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = dbSet.AsQueryable();

        if (filter != null) query = query.Where(filter);

        return await query.ToListAsync();
    }

    public async Task CreateAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(Expression<Func<T, bool>> idFilter)
    {
        var existingEntity = await dbSet.Where(idFilter)
                                        .FirstOrDefaultAsync()
                                        ?? throw new ResourceNotFoundException();

        dbSet.Remove(existingEntity);

        await SaveChangesAsync();
    } 

    private async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();    
}