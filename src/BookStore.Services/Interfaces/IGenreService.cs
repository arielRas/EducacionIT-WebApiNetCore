using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IGenreService
{
    Task<GenreDto> GetByCodeAsync(string code);
    Task<IEnumerable<GenreDto>> GetAllAsync();
    Task CreateAsync(GenreDto genre);
    Task UpdateAsync(GenreDto genre);
    Task DeleteAsync(string code);
}
