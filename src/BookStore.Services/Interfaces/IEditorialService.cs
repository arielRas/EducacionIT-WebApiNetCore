using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IEditorialService
{
    Task<EditorialDto> GetByIdAsync(int id);
    Task<IEnumerable<EditorialDto>> GetAllAsync();
    Task<EditorialDto> CreateAsync(EditorialDto editorial);
    Task UpdateAsync(int id, EditorialDto editorial);
    Task DeleteAsync(int id);
}
