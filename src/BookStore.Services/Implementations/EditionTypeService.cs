using BookStore.Data.Repository.Interfaces;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;
using Microsoft.Extensions.Logging;

namespace BookStore.Services.Implementations;

public class EditionTypeService : IEditionTypeService
{
    private readonly IEditionTypeRepository _repository;
    private readonly ILogger _logger;

    public EditionTypeService(IEditionTypeRepository repository)
        => _repository = repository;

    public async Task<EditionTypeDto> GetByCodeAsync(string code)
    {
        try
        {
            return (await _repository.GetByCodeAsync(code)).ToDto();
        }
        catch (Exception)
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
        catch (Exception)
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
        catch (Exception)
        {
            throw;
        }

    }

    public async Task UpdateAsync(string code, EditionTypeDto type)
    {
        try
        {
            await _repository.UpdateAsync(code, type.ToDao());
        }
        catch (Exception)
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
        catch (Exception)
        {
            throw;
        }
    }   
}
