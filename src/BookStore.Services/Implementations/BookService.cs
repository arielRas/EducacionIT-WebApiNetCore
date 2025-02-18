using System;
using BookStore.Common.Exceptions;
using BookStore.Data.Repository.Interfaces;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;

namespace BookStore.Services.Implementations;

public class BookService : IBookService
{   
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
        => _repository = repository;


    public async Task<BookResponseDto> GetByIdAsync(int id)
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

    public async Task<IEnumerable<BookDto>> GetAllAsync()
    {
        try
        {
            return (await _repository.GetAllAsync()).Select(b => b.ToDto());
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

    public async Task<IEnumerable<BookDto>> GetAllFilteredAsync(string filter)
    {
        try
        {
            return (await _repository.GetAllFilteredByTitleOrAuthorAsync(filter)).Select(b => b.ToDto());
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

    public async Task<BookDto> CreateAsync(BookDto book)
    {
        try
        {
            var bookDao = book.ToDao();

            await _repository.CreateAsync(bookDao);

            return bookDao.ToDto();
        }
        catch(Exception)
        {
            throw;
        }
    }

    public async Task UpdateAsync(int id, BookDto book)
    {
        try
        {
            await _repository.UpdateAsync(id, book.ToDao());
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
