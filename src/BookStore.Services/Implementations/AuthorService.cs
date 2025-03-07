using System;
using BookStore.Common.Exceptions;
using BookStore.Data.Repository.Interfaces;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;
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
        catch(ResourceNotFoundException ex) 
        {
            _logger.LogWarning(ex, $"{nameof(GetByIdAsync)} method, Error trying to get resource with id {id}");

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
        catch(ResourceNotFoundException ex) 
        {
            _logger.LogWarning(ex, $"{nameof(GetAllAsync)} method, does not return any resources");

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
        catch(Exception)
        {
            throw;
        }
    }

    public async Task UpdateAsync(int id, AuthorDto author)
    {
        try
        {
            author.Id = id;

            await _repository.UpdateAsync(id, author.ToDao());
        }
        catch(ResourceNotFoundException ex) 
        {
            _logger.LogWarning(ex, $"{nameof(UpdateAsync)} method, Error trying to get resource with id {id}");

            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            await _repository.DeleteAsync(id);
        }
        catch(ResourceNotFoundException ex) 
        {
            _logger.LogWarning(ex, $"{nameof(UpdateAsync)} method, Error trying to get resource with id {id}");

            throw;
        }
    }       
}