using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IAuthorService
{
    Task<AuthorDto> GetByIdAsync(int id);    
    Task<IEnumerable<AuthorDto>> GetAllAsync();
    Task<IEnumerable<AuthorDto>> GetAllFilteredAsync(string filter);
    Task<AuthorDto> CreateAsync(AuthorDto author);
    Task UpdateAsync(int id, AuthorDto author);
    Task DeleteAsync(int id);
}