using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IGenreService
{
    Task<GenreDto> GetByCodeAsync(string code);
    Task<IEnumerable<GenreDto>> GetAllAsync();
    Task<GenreDto> CreateAsync(GenreDto genre);
    Task UpdateAsync(string code, GenreDto genre);
    Task DeleteAsync(string code);
}
