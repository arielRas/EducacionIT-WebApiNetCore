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

public class EditorialService : IEditorialService
{
    private readonly IEditorialRepository _repository;
    private readonly ILogger _logger;

    public EditorialService(IEditorialRepository repository, ILogger<EditorialService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

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
    }

    public async Task<EditorialDto> CreateAsync(EditorialDto editorial)
    {
        try
        {
            var editorialDao = editorial.ToDao();

            await _repository.CreateAsync(editorialDao);

            return editorialDao.ToDto();
        }
        catch (DbUpdateException ex)
        {
            var message = "Error trying to create resource";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
        }
    }

    public async Task UpdateAsync(int id, EditorialDto editorial)
    {
        try
        {
            editorial.Id = id;

            await _repository.UpdateAsync(id, editorial.ToDao());
        }
        catch (DbUpdateException ex)
        {
            var message = $"Error trying to update resource with id {id}";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
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
            var message = $"Error trying to update resource with id {id}";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
        }
    }   
}
