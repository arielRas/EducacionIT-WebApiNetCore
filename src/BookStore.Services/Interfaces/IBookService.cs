using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IBookService
{
    Task<BookResponseDto> GetByIdAsync(int id);    
    Task<IEnumerable<BookDto>> GetAllAsync();
    Task<IEnumerable<BookDto>> GetAllFilteredAsync(string filter);
    Task<BookDto> CreateAsync(BookRequestDto book);
    Task UpdateAsync(int id, BookDto book);
    Task UpdateGenresAsync(int bookId, IEnumerable<string> genreCodes);
    Task UpdateAuthorsAsync(int bookId, IEnumerable<int> authorIds);
    Task DeleteAsync(int id);
}
