using System;
using System.Linq.Expressions;

namespace BookStore.Data.Repository.Interfaces;

public interface IRepository<T> where T : class 
{
    Task<T> GetByIdAsync(Expression<Func<T, bool>> idFilter);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    Task CreateAsync(T entity);
    Task DeleteAsync(Expression<Func<T, bool>> idFilter);
}