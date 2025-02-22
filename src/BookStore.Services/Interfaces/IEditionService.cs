using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IEditionService
{
    Task<EditionDto> GetByIdAsync(Guid id);    
    Task<IEnumerable<EditionDto>> GetAllAsync();
    Task<IEnumerable<EditionDto>> GetByBookTitle(string bookTitle);
    Task<EditionDto> CreateAsync(EditionRequestDto edition);
    Task UpdateAsync(Guid id, EditionDto edition);
    Task UpdateEditionPriceAsync(Guid id, decimal price);
    Task UpdateEditionEditorialAsync(Guid id, int editorialId);
    Task DeleteAsync(Guid id);
}
