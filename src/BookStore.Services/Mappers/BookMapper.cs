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

    public static BookResponseDto ToResponseDto(this Book dao)
    {
        return new BookResponseDto
        {
            Id = dao.BookId,
            Title = dao.Title,
            Synopsis = !string.IsNullOrWhiteSpace(dao.Synopsis) ? dao.Synopsis : null,
            Genres = dao.Genres.Select(g => g.ToDto()).ToList(),
            Authors = dao.Authors.Select(a => a.ToDto()).ToList(),            
        };
    }

    public static Book ToDao(this Book dto)
    {
        return new Book
        {
            BookId = dto.BookId,
            Title = dto.Title,
            Synopsis = !string.IsNullOrWhiteSpace(dto.Synopsis) ? dto.Synopsis : null
        };
    }
}