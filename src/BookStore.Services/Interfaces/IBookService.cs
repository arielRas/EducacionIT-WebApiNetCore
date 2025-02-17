using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IBookService
{
    Task<BookResponseDto> GetByIdAsync(int id);    
    Task<IEnumerable<BookDto>> GetAllAsync();
    Task<BookDto> GetAllFilteredAsync(string filter);
    Task CreateAsync(BookDto book);
    Task UpdateAsync(int id, BookDto author);
    Task DeleteAsync(int id);
}
