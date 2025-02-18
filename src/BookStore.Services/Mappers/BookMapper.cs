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
            Genres = dao.Genres.Select(g => g.Code).ToList(),
            Authors = dao.Authors.Select(a => a.ToDto()).ToList(),
            Editions = dao.Editions?.Select(e => e.EditionType.Code).ToList()         
        };
    }

    public static Book ToCreateDao(this BookRequestDto dto, IEnumerable<Author> authors, IEnumerable<Genre> genres)
    {
        return new Book
        {
            BookId = dto.Id,
            Title = dto.Title,
            Synopsis = !string.IsNullOrWhiteSpace(dto.Synopsis) ? dto.Synopsis : null,
            Genres = genres.ToList(),
            Authors = authors.ToList()
        };
    }
}