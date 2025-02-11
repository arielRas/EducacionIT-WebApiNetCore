using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IEditorialService
{
    Task<EditorialDto> GetByIdAsync(int id);
    Task<IEnumerable<EditorialDto>> GetAllAsync();
    Task CreateAsync(EditorialDto editorial);
    Task UpdateAsync(EditorialDto editorial);
    Task DeleteAsync(int id);
}
