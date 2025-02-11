using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IEditionTypeService
{
    Task<EditionTypeDto> GetByCodeAsync(string code);
    Task<IEnumerable<EditionTypeDto>> GetAllAsync();
    Task CreateAsync(EditionTypeDto type);
    Task UpdateAsync(EditionTypeDto type);
    Task DeleteAsync(string code);
}