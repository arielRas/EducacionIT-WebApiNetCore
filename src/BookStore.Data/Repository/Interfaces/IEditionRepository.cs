using System;
using System.Linq.Expressions;
using BookStore.Data.Databases.BookStoreDb.Entities;

namespace BookStore.Data.Repository.Interfaces;

public interface IEditionRepository
{
    Task<Edition> GetByIdAsync(Guid id);
    Task<IEnumerable<Edition>> GetAllAsync();
    Task<IEnumerable<Edition>> GetAllFilteredAsync(Expression<Func<Edition,bool>> filter);
    Task CreateAsync(Edition edition);
    Task UpdateAsync(Guid id, Edition edition);
    Task DeleteAsync(Guid id);
}
