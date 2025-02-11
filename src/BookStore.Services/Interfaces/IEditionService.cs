using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IEditionService
{
    Task<EditionDto> GetByIdAsync(Guid id);    
    Task<IEnumerable<EditionDto>> GetAllAsync();
    Task<EditionDto> GetFilteredAsync(string filter);
    Task CreateAsync(EditionDto author);
    Task UpdateAsync(EditionDto author);
    Task DeleteAsync(Guid id);
}
