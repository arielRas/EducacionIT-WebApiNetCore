using System;
using BookStore.Common.Exceptions;
using BookStore.Data.Repository.Interfaces;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;

namespace BookStore.Services.Implementations;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;

    public AuthorService(IAuthorRepository repository)
        => _repository = repository;

    public async Task<AuthorDto> GetByIdAsync(int id)
    {        
        try
        {
            return (await _repository.GetByIdWithRelationshipsAsync(id)).ToResponseDto();
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
        catch(Exception)
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
        catch(Exception)
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
        catch(Exception)
        {
            throw;
        }
    }

    public async Task UpdateAsync(int id, AuthorDto author)
    {
        try
        {
            await _repository.UpdateAsync(id, author.ToDao());
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

    public async Task DeleteAsync(int id)
    {
        try
        {
            await _repository.DeleteAsync(id);
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
