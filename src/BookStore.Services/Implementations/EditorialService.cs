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
            var editorial = await _repository.GetByIdAsync(id);

            return EditorialMapper.ToDto(editorial);
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
            var editorials = await _repository.GetAllAsync();

            return editorials.Select(EditorialMapper.ToDto);
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
            var editorialDao = EditorialMapper.ToDao(editorial);

            await _repository.CreateAsync(editorialDao);

            return EditorialMapper.ToDto(editorialDao);
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
            await _repository.UpdateAsync(id, EditorialMapper.ToDao(editorial));
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
