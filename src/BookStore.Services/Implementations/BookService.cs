using System;
using System.ComponentModel.DataAnnotations;
using BookStore.Common.Exceptions;
using BookStore.Common.Validations;
using BookStore.Data.Repository.Interfaces;
using BookStore.Data.UnitOfWork.Interfaces;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;

namespace BookStore.Services.Implementations;

public class BookService : IBookService
{   
    private readonly IBookRepository _repository;
    private readonly IBookUnitOfWork _unitOfWork;

    public BookService(IBookRepository repository, IBookUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }


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
            filter = filter.ToLower().Trim();

            var books = await _repository.GetAllFilteredByTitleOrAuthorAsync(filter);

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
            var authors = await _unitOfWork.AuthorRepository.GetByIdsAsync(book.AuthorsId);

            var validation = ValidateCollections(book.AuthorsId, authors.Select(a => a.AuthorId));

            if (!validation.IsValid)
                throw new ResourceNotFoundException(validation.ErrorMessage);

            var genres = await _unitOfWork.GenreRepository.GetByCodesAsync(book.Genres);

            validation = ValidateCollections(book.Genres, genres.Select(g => g.Code));

            if (!validation.IsValid)
                throw new ResourceNotFoundException(validation.ErrorMessage);

            var bookDao = book.ToCreateDao(authors, genres);

            await _unitOfWork.BookRepository.CreateAsync(bookDao);

            await _unitOfWork.SaveChangesAsync();

            return bookDao.ToDto();
        }
        catch(ResourceNotFoundException) 
        {
            _unitOfWork.Dispose();
            throw;
        }
        catch(Exception)
        {
            _unitOfWork.Dispose();
            throw;
        }
    }

    public async Task UpdateAsync(int id, BookDto book)
    {
        try
        {
            book.Id = id;

            await _repository.UpdateAsync(id, book.ToDao());

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

    private ResultValidation ValidateCollections<T>(IEnumerable<T> dtoList,  IEnumerable<T> daoList)
    {
        var nonExistingElements = dtoList.Except(daoList);

        if(!nonExistingElements.Any()) 
            return new ResultValidation();

        string errorMessage = string.Join(", ", nonExistingElements.Select(e => e!.ToString()));

        return new ResultValidation($"Resources with {errorMessage} do not exist");        
    }
}
