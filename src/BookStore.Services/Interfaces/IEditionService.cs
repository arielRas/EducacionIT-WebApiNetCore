using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IEditionService
{
    Task<EditionDto> GetByIdAsync(Guid id);    
    Task<IEnumerable<EditionDto>> GetAllAsync();
    Task<IEnumerable<EditionDto>> GetByBookTitle(string bookTitle);
    Task<EditionDto> CreateAsync(EditionRequestCreateDto edition);
    Task UpdateAsync(Guid id, EditionRequestUpdateDto edition);
    Task UpdateEditionPriceAsync(Guid id, decimal price);
    Task UpdateIsbnAsync(Guid id, string isbn);
    Task DeleteAsync(Guid id);
}
