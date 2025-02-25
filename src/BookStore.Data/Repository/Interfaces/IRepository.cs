using System;
using System.Linq.Expressions;

namespace BookStore.Data.Repository.Interfaces;

public interface IRepository<T> where T : class 
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    Task CreateAsync(T entity);
    Task UpdateAsync(int id, T entity);
    Task DeleteAsync(int id);
}