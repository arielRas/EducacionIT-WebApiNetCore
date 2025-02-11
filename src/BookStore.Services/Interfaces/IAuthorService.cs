using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IAuthorService
{
    Task<AuthorDto> GetByIdAsync(int id);    
    Task<IEnumerable<AuthorDto>> GetAllAsync();
    Task<AuthorDto> GetFilteredAsync(string filter);
    Task CreateAsync(AuthorDto author);
    Task UpdateAsync(AuthorDto author);
    Task DeleteAsync(int id);
}