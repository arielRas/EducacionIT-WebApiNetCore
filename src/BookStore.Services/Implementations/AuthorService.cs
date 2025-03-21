using BookStore.Data.Repository.Interfaces;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;
using Microsoft.Extensions.Logging;

namespace BookStore.Services.Implementations;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    

    public AuthorService(IAuthorRepository repository, ILogger<AuthorService> logger)
        => _repository = repository;

    public async Task<AuthorDto> GetByIdAsync(int id)
    {        
        try
        {
            var author = await _repository.GetByIdAsync(id);
            
            return author.ToResponseDto();
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
            author.Id = id;

            await _repository.UpdateAsync(id, author.ToDao());
        }
        catch (Exception)
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
        catch (Exception)
        {
            throw;
        }
    }       
}