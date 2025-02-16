using System;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Services.DTOs;

namespace BookStore.Services.Mappers;

internal static class BookMapper
{
    public static BookDto ToDto(this Book dao)
    {
        return new BookDto
        {
            Id = dao.BookId,
            Title = dao.Title,
            Synopsis = !string.IsNullOrWhiteSpace(dao.Synopsis) ? dao.Synopsis : null
        };
    }
}
