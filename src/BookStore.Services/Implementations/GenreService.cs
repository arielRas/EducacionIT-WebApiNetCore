using System;
using BookStore.Common.Exceptions;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Data.Repository.Interfaces;
using BookStore.Data.Repository.Repositories;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;

namespace BookStore.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;

    public GenreService(IGenreRepository repository)
        => _repository = repository;

    public async Task<GenreDto> GetByCodeAsync(string code)
    {
        try
        {
            var genre = await _repository.GetByCodeAsync(code);

            return GenreMapper.ToDto(genre);
        }
        catch(ResourceNotFoundException) 
        {
            throw;
        }
        catch(Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<GenreDto>> GetAllAsync()
    {
        try
        {
            var genres = await _repository.GetAllAsync();

            return genres.Select(GenreMapper.ToDto);
        }
        catch(ResourceNotFoundException) 
        {
            throw;
        }
        catch(Exception)
        {
            throw;
        }
    }

    public async Task<GenreDto> CreateAsync(GenreDto genre)
    {
        try
        {
            var genreDao = GenreMapper.ToDao(genre);

            await _repository.CreateAsync(genreDao);

            return GenreMapper.ToDto(genreDao);
        }
        catch(Exception)
        {
            throw;
        }
    }

    public async Task UpdateAsync(string code, GenreDto genre)
    {
        try
        {
            await _repository.UpdateAsync(code, GenreMapper.ToDao(genre));
        }
        catch(ResourceNotFoundException) 
        {
            throw;
        }
        catch(Exception)
        {
            throw;
        }
    }
    
    public async Task DeleteAsync(string code)
    {
        try
        {
            await _repository.DeleteAsync(code);
        }
        catch(ResourceNotFoundException) 
        {
            throw;
        }
        catch(Exception)
        {
            throw;
        }
    }    
}
