using BookStore.Data.Repository.Interfaces;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;

namespace BookStore.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;

    public GenreService(IGenreRepository repository)
        => _repository = repository;

    public async Task<GenreDto> GetByCodeAsync(string code)
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

    public async Task<IEnumerable<GenreDto>> GetAllAsync()
    {
        try
        {
            return (await _repository.GetAllAsync()).Select(g => g.ToDto());
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<GenreDto> CreateAsync(GenreDto genre)
    {
        try
        {
            var genreDao = genre.ToDao();

            await _repository.CreateAsync(genreDao);

            return genreDao.ToDto();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task UpdateAsync(string code, GenreDto genre)
    {
        try
        {
            await _repository.UpdateAsync(code, genre.ToDao());
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
