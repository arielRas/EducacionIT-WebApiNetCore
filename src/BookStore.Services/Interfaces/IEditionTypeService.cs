using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IEditionTypeService
{
    Task<EditionTypeDto> GetByCodeAsync(string code);
    Task<IEnumerable<EditionTypeDto>> GetAllAsync();
    Task<EditionTypeDto> CreateAsync(EditionTypeDto type);
    Task UpdateAsync(string code, EditionTypeDto type);
    Task DeleteAsync(string code);
}