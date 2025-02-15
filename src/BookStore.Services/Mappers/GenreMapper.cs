using System;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Services.DTOs;

namespace BookStore.Services.Mappers;

internal static class GenreMapper
{
    public static Genre ToDao(this GenreDto dto)
    {
        return new Genre
        {
            Code = dto.Code,
            Name = dto.Name
        };
    }

    public static GenreDto ToDto(this Genre dao)
    {
        return new GenreDto
        {
            Code = dao.Code,
            Name = dao.Name
        };
    }
}