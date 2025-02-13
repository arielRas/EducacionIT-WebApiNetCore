using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IEditionService
{
    Task<EditionDto> GetByIdAsync(Guid id);    
    Task<IEnumerable<EditionDto>> GetAllAsync();
    Task<IEnumerable<EditionDto>> GetAllFilteredAsync(string filter);
    Task CreateAsync(EditionDto edition);
    Task UpdateAsync(Guid id, EditionDto edition);
    Task DeleteAsync(Guid id);
}
