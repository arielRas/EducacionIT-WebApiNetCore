using System;
using BookStore.Common.Exceptions;
using BookStore.Data.Repository.Interfaces;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;

namespace BookStore.Services.Implementations;

public class EditionTypeService : IEditionTypeService
{
    private readonly IEditionTypeRepository _repository;

    public EditionTypeService(IEditionTypeRepository repository)
        => _repository = repository;

    public async Task<EditionTypeDto> GetByCodeAsync(string code)
    {
        try
        {
            var editionType = await _repository.GetByCodeAsync(code);

            return EditionTypeMapper.ToDto(editionType);
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

    public async Task<IEnumerable<EditionTypeDto>> GetAllAsync()
    {
        try
        {
            var editionTypes = await _repository.GetAllAsync();

            return editionTypes.Select(EditionTypeMapper.ToDto);
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

    public async Task<EditionTypeDto> CreateAsync(EditionTypeDto type)
    {
        try
        {
            var editionTypeDao = EditionTypeMapper.ToDao(type);

            await _repository.CreateAsync(editionTypeDao);

            return EditionTypeMapper.ToDto(editionTypeDao);
        }
        catch(Exception)
        {
            throw;
        }
    }

    public async Task UpdateAsync(string code, EditionTypeDto type)
    {
        try
        {
            await _repository.UpdateAsync(code, EditionTypeMapper.ToDao(type));
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
