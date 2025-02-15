using System;
using BookStore.Common.Exceptions;
using BookStore.Data.Repository.Interfaces;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;

namespace BookStore.Services.Implementations;

public class EditorialService : IEditorialService
{
    private readonly IEditorialRepository _repository;

    public EditorialService(IEditorialRepository repository)
        => _repository = repository;

    public async Task<EditorialDto> GetByIdAsync(int id)
    {
        try
        {
            return (await _repository.GetByIdAsync(id)).ToDto();
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

    public async Task<IEnumerable<EditorialDto>> GetAllAsync()
    {
        try
        {
            return (await _repository.GetAllAsync()).Select(e => e.ToDto());
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

    public async Task<EditorialDto> CreateAsync(EditorialDto editorial)
    {
        try
        {
            var editorialDao = editorial.ToDao();

            await _repository.CreateAsync(editorialDao);

            return editorialDao.ToDto();
        }
        catch(Exception)
        {
            throw;
        }
    }

    public async Task UpdateAsync(int id, EditorialDto editorial)
    {
        try
        {
            await _repository.UpdateAsync(id, editorial.ToDao());
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
