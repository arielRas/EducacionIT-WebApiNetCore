using System;
using System.Linq.Expressions;
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
using BookStore.Services.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Services.Implementations;

public class EditionService : IEditionService
{
    private readonly IEditionRepository _repository;
    private readonly IEditionUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public EditionService(IEditionRepository repository, IEditionUnitOfWork unitOfWork, ILogger<EditionService> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<EditionDto> GetByIdAsync(Guid id)
    {
        try
        {
            var edition = await _repository.GetByIdAsync(id);

            return edition.ToResposeDto();
        }
        catch(ResourceNotFoundException)
        {
            throw;
        }
    }


    public async Task<IEnumerable<EditionDto>> GetAllAsync()
    {
        try
        {
            var editions = await _repository.GetAllAsync();

            return editions.Select(e => e.ToDto());
        }
        catch(ResourceNotFoundException)
        {
            throw;
        }
    }


    public async Task<IEnumerable<EditionDto>> GetByBookTitle(string bookTitle)
    {
        try
        {
            Expression<Func<Edition, bool>> filter = e => e.Book.Title.Contains(bookTitle);

            var editions = await _repository.GetAllFilteredAsync(filter);

            return editions.Select(e => e.ToDto());
        }
        catch(ResourceNotFoundException)
        {
            throw;
        }       
    }


    public async Task<EditionDto> CreateAsync(EditionRequestCreateDto edition)
    {
        try
        {
            var validationResult = edition.Validate();

            if(!validationResult.IsValid)
                throw new BusinessException(validationResult.ErrorMessage);

            validationResult = await ValidateRelationships(edition);

            if(!validationResult.IsValid)
                throw new BusinessException(validationResult.ErrorMessage);

            var editionDao = edition.ToDao();

            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.EditionRepository.CreateAsync(editionDao);

            await _unitOfWork.EditionPriceRepository.CreateAsync(CreateEditionPrice(editionDao, edition.Price));

            if(edition.Isbn is not null)
                await _unitOfWork.IsbnRepository.CreateAsync(CreateIsbn(editionDao, edition.Isbn));

            await _unitOfWork.CommitTransactionAsync();            

            return editionDao.ToDto(); 
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


    public async Task UpdateAsync(Guid id, EditionRequestUpdateDto edition)
    {
        try 
        {
            edition.Id = id;

            var resultValidation = edition.Validate();

            if(!resultValidation.IsValid)
                throw new BusinessException(resultValidation.ErrorMessage);

            resultValidation = await ValidateRelationships(edition);

            if (!resultValidation.IsValid)
                throw new BusinessException(resultValidation.ErrorMessage);

            await _unitOfWork.EditionRepository.UpdateAsync(id, edition.ToDao());
        }
        catch (DbUpdateException ex)
        {
            var message = $"Error trying to update resource with Id {id}";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
        }
    }


    public async Task UpdateIsbnAsync(Guid id, string isbn)
    {
        try
        {
            await _unitOfWork.IsbnRepository.UpdateByEditionIdAsync(id, isbn);

        }
        catch (DbUpdateException ex)
        {
            var message = $"Error trying to update ISBN for Edition id {id}";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
        }
    }


    public async Task UpdateEditionPriceAsync(Guid id, decimal price)
    {
        try
        {
            await _unitOfWork.EditionPriceRepository.UpdateAsync(id, price);                        
        }
        catch (DbUpdateException ex)
        {
            var message = $"Error trying to update Price for Edition id {id}";

            throw _logger.HandleAndThrow(ex, MethodBase.GetCurrentMethod()!.Name, message);
        }
    }


    public async Task DeleteAsync(Guid id)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.EditionRepository.DeleteAsync(id);

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

    private async Task<ResultValidation> ValidateRelationships(EditionRequestUpdateDto dto)
    {
        var existBook = await _unitOfWork.BookRepository.Exist(dto.BookId);
        
        if(!existBook)
            return new ResultValidation($"The book with id: {dto.BookId} does not exist");

        var existEditorial = await _unitOfWork.EditorialRepository.Exist(dto.EditorialId);

        if(!existEditorial)
            return new ResultValidation($"The editorial with id: {dto.EditorialId} does not exist");

        return new ResultValidation();
    }


    private EditionPrice CreateEditionPrice(Edition edition, decimal price)
    {
        return new EditionPrice
        {
            Price = price,
            Edition = edition
        };
    }

    private Isbn CreateIsbn(Edition edition, string isbnCode)
    {
        return new Isbn
        {
            Code = isbnCode,
            Edition = edition
        };
    }    
}
