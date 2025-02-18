using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IBookService
{
    Task<BookResponseDto> GetByIdAsync(int id);    
    Task<IEnumerable<BookDto>> GetAllAsync();
    Task<IEnumerable<BookDto>> GetAllFilteredAsync(string filter);
    Task<BookDto> CreateAsync(BookDto book);
    Task UpdateAsync(int id, BookDto book);
    Task DeleteAsync(int id);
}
