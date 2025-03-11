using BookStore.Common.Exceptions;
using BookStore.Data.Repository.Interfaces;
using BookStore.Services.DTOs;
using BookStore.Services.Extensions;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace BookStore.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;
    private readonly ILogger _logger;

    public GenreService(IGenreRepository repository, ILogger<GenreService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<GenreDto> GetByCodeAsync(string code)
    {
        try
        {
            return (await _repository.GetByCodeAsync(code)).ToDto();
        }
        catch(ResourceNotFoundException) 
        {
            throw;
        }
    }

    public async Task<IEnumerable<GenreDto>> GetAllAsync()
    {
        try
        {
            return (await _repository.GetAllAsync()).Select(g => g.ToDto());
        }
        catch(ResourceNotFoundException) 
        {
            throw;
        }
    }

    public async Task<GenreDto> CreateAsync(GenreDto genre)
    {
        try
        {
            var genreDao = genre.ToDao();

            await _repository.CreateAsync(genreDao);

            return genreDao.ToDto();
        }
        catch (DbUpdateException ex)
        {
            var message = "Error trying to create resource";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
        }
    }

    public async Task UpdateAsync(string code, GenreDto genre)
    {
        try
        {
            await _repository.UpdateAsync(code, genre.ToDao());
        }
        catch (DbUpdateException ex)
        {
            var message = $"Error trying to update resource with code {code}";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
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
        catch (DbUpdateException ex)
        {
            var message = $"Error trying to update resource with code {code}";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
        }
    }    
}
