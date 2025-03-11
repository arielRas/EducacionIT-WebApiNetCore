using System;
using System.Net;
using System.Reflection;
using BookStore.Common.Exceptions;
using BookStore.Common.Validations;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Data.Repository.Interfaces;
using BookStore.Data.UnitOfWork.Interfaces;
using BookStore.Services.DTOs;
using BookStore.Services.Extensions;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Services.Implementations;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IBookUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public BookService(IBookRepository repository, IBookUnitOfWork unitOfWork, ILogger<BookService> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<BookResponseDto> GetByIdAsync(int id)
    {
        try
        {
            return (await _repository.GetByIdAsync(id)).ToResponseDto();
        }
        catch (ResourceNotFoundException)
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
        catch (ResourceNotFoundException)
        {
            throw;
        }
    }


    public async Task<IEnumerable<BookDto>> GetAllFilteredAsync(string filter)
    {
        try
        {
            filter = filter.ToLower().Trim();

            var books = await _repository.GetFilteredByTitleOrAuthorAsync(filter);

            return books.Select(b => b.ToDto());
        }
        catch (ResourceNotFoundException)
        {
            throw;
        }
    }


    public async Task<BookDto> CreateAsync(BookRequestDto book)
    {
        try
        {
            var authors = await _unitOfWork.AuthorRepository.GetByIdsAsync(book.AuthorsId);

            var resultValidation = ValidateCollections(book.AuthorsId, authors.Select(a => a.AuthorId));

            if (!resultValidation.IsValid)
                throw new ResourceNotFoundException(resultValidation.ErrorMessage);

            var genres = await _unitOfWork.GenreRepository.GetByCodesAsync(book.Genres);

            resultValidation = ValidateCollections(book.Genres, genres.Select(g => g.Code));

            if (!resultValidation.IsValid)
                throw new ResourceNotFoundException(resultValidation.ErrorMessage);

            var bookDao = book.ToCreateDao(authors, genres);

            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.BookRepository.CreateAsync(bookDao);

            await _unitOfWork.CommitTransactionAsync();

            return bookDao.ToDto();
        }
        catch (DbUpdateException ex)
        {
            await _unitOfWork.RollbackTransactionAsync();

            var message = "Error trying to create resource";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }     
    }


    public async Task UpdateAsync(int id, BookDto book)
    {
        try
        {
            book.Id = id;

            await _repository.UpdateAsync(id, book.ToDao());
        }
        catch (ResourceNotFoundException)
        {
            throw;
        }
        catch (DbUpdateException ex)
        {
            var message = $"Error trying to update resource with Id {id}";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
        }
    }


    public async Task UpdateGenresAsync(int bookId, IEnumerable<string> genreCodes)
    {
        try
        {  
            var genres = await _unitOfWork.GenreRepository.GetByCodesAsync(genreCodes);

            var resultValidation = ValidateCollections(genreCodes, genres.Select(g => g.Code));

            if (!resultValidation.IsValid)
                throw new ResourceNotFoundException(resultValidation.ErrorMessage);

            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.BookRepository.UpdateGenresAsync(bookId, genres);

            await _unitOfWork.CommitTransactionAsync();
        }
        catch (DbUpdateException ex)
        {
            await _unitOfWork.RollbackTransactionAsync();

            var message = $"Error trying to update genres for book id {bookId}";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }


    public async Task UpdateAuthorsAsync(int bookId, IEnumerable<int> authorIds)
    {
        try
        {
            var authors = await _unitOfWork.AuthorRepository.GetByIdsAsync(authorIds);

            var resultValidation = ValidateCollections(authorIds, authors.Select(a => a.AuthorId));

            if (!resultValidation.IsValid)
                throw new ResourceNotFoundException(resultValidation.ErrorMessage);

            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.BookRepository.UpdateAuthorsAsync(bookId, authors);

            await _unitOfWork.CommitTransactionAsync();
        }
        catch (DbUpdateException ex)
        {
            await _unitOfWork.RollbackTransactionAsync();

            var message = $"Error trying to update genres for book id {bookId}";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }


    public async Task DeleteAsync(int id)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.BookRepository.DeleteAsync(id);

            await _unitOfWork.CommitTransactionAsync();
        }
        catch (DbUpdateException ex)
        {
            await _unitOfWork.RollbackTransactionAsync();

            var message = $"Error trying to delete resource with id {id}";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }


  
    private ResultValidation ValidateCollections<T>(IEnumerable<T> dtoList, IEnumerable<T> daoList)
    {
        var nonExistingElements = dtoList.Except(daoList).ToList();

        if (!nonExistingElements.Any())
            return new ResultValidation();

        string errorMessage = string.Join(", ", nonExistingElements.Select(e => e!.ToString()));

        return new ResultValidation($"Resources with {errorMessage} do not exist");
    }
}
