using System;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Services.DTOs;

namespace BookStore.Services.Mappers;

internal static class AuthorMapper
{
    public static Author ToDao(this AuthorDto dto)
    {
        return new Author
        {
            AuthorId = dto.Id,
            Name = dto.Name,
            LastName = !string.IsNullOrWhiteSpace(dto.LastName) ? dto.LastName : null
        };
    }

    public static AuthorDto ToDto(this Author dao)
    {
        return new AuthorDto
        {
            Id = dao.AuthorId,
            Name = dao.Name,
            LastName = !string.IsNullOrWhiteSpace(dao.LastName) ? dao.LastName : string.Empty
        };
    }

    public static AuthorResponseDto ToResponseDto(this Author dao)
    {
        return new AuthorResponseDto
        {
            Id = dao.AuthorId,
            Name = dao.Name,
            LastName = !string.IsNullOrWhiteSpace(dao.LastName) ? dao.LastName : string.Empty,
            Books = dao.Book.Any()
                    ? dao.Book.Select(b => b.ToDto()).ToList()
                    : null
        };
    }
}
