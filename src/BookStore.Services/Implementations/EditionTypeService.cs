using System;
using System.Reflection;
using BookStore.Common.Exceptions;
using BookStore.Data.Repository.Interfaces;
using BookStore.Services.DTOs;
using BookStore.Services.Extensions;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Services.Implementations;

public class EditionTypeService : IEditionTypeService
{
    private readonly IEditionTypeRepository _repository;
    private readonly ILogger _logger;

    public EditionTypeService(IEditionTypeRepository repository, ILogger<EditionTypeService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<EditionTypeDto> GetByCodeAsync(string code)
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

    public async Task<IEnumerable<EditionTypeDto>> GetAllAsync()
    {
        try
        {
            var editionTypes = await _repository.GetAllAsync();

            return editionTypes.Select(e => e.ToDto());
        }
        catch(ResourceNotFoundException) 
        {
            throw;
        }
    }

    public async Task<EditionTypeDto> CreateAsync(EditionTypeDto type)
    {
        try
        {
            var editionTypeDao = type.ToDao();

            await _repository.CreateAsync(editionTypeDao);

            return editionTypeDao.ToDto();
        }
        catch (DbUpdateException ex)
        {
            var message = "Error trying to create resource";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
        }

    }

    public async Task UpdateAsync(string code, EditionTypeDto type)
    {
        try
        {
            await _repository.UpdateAsync(code, type.ToDao());
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
        catch (DbUpdateException ex)
        {
            var message = $"Error trying to update resource with code {code}";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
        }
    }   
}
