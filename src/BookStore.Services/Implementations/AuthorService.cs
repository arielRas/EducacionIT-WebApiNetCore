using System;
using BookStore.Common.Exceptions;
using BookStore.Data.Repository.Interfaces;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Services.Implementations;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly ILogger _logger;
    

    public AuthorService(IAuthorRepository repository, ILogger<AuthorService> logger)
    {
        _repository = repository;
        _logger = logger;
    } 

    public async Task<AuthorDto> GetByIdAsync(int id)
    {        
        try
        {
            var author = await _repository.GetByIdAsync(id);
            
            return author.ToResponseDto();
        }
        catch(ResourceNotFoundException) 
        {
            throw;
        }
    }

    public async Task<IEnumerable<AuthorDto>> GetAllFilteredAsync(string filter)
    {
        try
        {
            return (await _repository.GetAllFilteredAsync(filter)).Select(a => a.ToDto());
        }
        catch(ResourceNotFoundException) 
        {
            throw;
        }
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAsync()
    {        
        try
        {
            return (await _repository.GetAllAsync()).Select(a => a.ToDto());
        }
        catch(ResourceNotFoundException) 
        {
            throw;
        }      
    } 

    public async Task<AuthorDto> CreateAsync(AuthorDto author)
    {
        try
        {
            var authorDao = author.ToDao();

            await _repository.CreateAsync(authorDao);

            return authorDao.ToDto();
        }
        catch(DbUpdateException ex)
        {
            var traceId = Guid.NewGuid();

            var message = "Error trying to create new resource";

            _logger.LogError(ex, $"{traceId} - {nameof(CreateAsync)} method, {message}");

            throw new DataBaseException(message, traceId);
        }
    }

    public async Task UpdateAsync(int id, AuthorDto author)
    {
        try
        {
            author.Id = id;

            await _repository.UpdateAsync(id, author.ToDao());
        }
        catch (DbUpdateException ex)
        {
            var traceId = Guid.NewGuid();

            var message = $"Error trying to update resource with Id {id}";

            _logger.LogError(ex, $"{traceId} - {nameof(UpdateAsync)} method, {message}");

            throw new DataBaseException(message, traceId);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            await _repository.DeleteAsync(id);
        }
        catch (DbUpdateException ex)
        {
            var traceId = Guid.NewGuid();

            var message = $"Error trying to delete resource with Id {id}";

            _logger.LogError(ex, $"{traceId} - {nameof(DeleteAsync)} method, {message}");

            throw new DataBaseException(message, traceId);
        }        
    }       
}