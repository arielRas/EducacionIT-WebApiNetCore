using System;
using BookStore.Common.Exceptions;
using BookStore.Data.Repository.Interfaces;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;

namespace BookStore.Services.Implementations;

public class BookService : IBookService
{   
    private readonly IBookRepository _bookRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IAuthorRepository _authorRepository;

    public BookService(IBookRepository repository, IAuthorRepository authorRepository, IGenreRepository genreRepository)
    {
        _bookRepository = repository;
        _genreRepository = genreRepository;
        _authorRepository = authorRepository;
    }


    public async Task<BookResponseDto> GetByIdAsync(int id)
    {
        try
        {
            return (await _bookRepository.GetByIdWithRelationshipsAsync(id)).ToResponseDto();
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
            return (await _bookRepository.GetAllAsync()).Select(b => b.ToDto());
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
            filter = filter.ToLower().Trim();

            var books = await _bookRepository.GetAllFilteredByTitleOrAuthorAsync(filter);

            return books.Select(b => b.ToDto());
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

    public async Task<BookDto> CreateAsync(BookRequestDto book)
    {
        try
        {
            var authors = await _authorRepository.GetAllFilteredByIdAsync(book.AuthorsId);

            var genres = await _genreRepository.GetAllFilteredByCodeAsync(book.Genres);

            var bookDao = book.ToCreateDao(authors, genres);

            await _bookRepository.CreateAsync(bookDao);

            return bookDao.ToDto();
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

    public async Task UpdateAsync(int id, BookDto book)
    {
        try
        {
            book.Id = id;

            await _bookRepository.UpdateAsync(id, book.ToDao());

            throw new NotImplementedException();
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
            await _bookRepository.DeleteAsync(id);
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
