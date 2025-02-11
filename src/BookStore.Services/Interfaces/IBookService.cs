using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IBookService
{
    Task<BookDto> GetByIdAsync(int id);    
    Task<IEnumerable<BookDto>> GetAllAsync();
    Task<BookDto> GetFilteredAsync(string filter);
    Task CreateAsync(BookDto author);
    Task UpdateAsync(BookDto author);
    Task DeleteAsync(int id);
}
