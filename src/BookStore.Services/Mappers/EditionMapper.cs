using System;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Services.DTOs;

namespace BookStore.Services.Mappers;

internal static class EditionMapper
{
    public static Edition ToDao(this EditionRequestUpdateDto dto)
    {
        return new Edition
        {
            EditionId = dto.Id,
            Pages = dto.Pages,
            PublicationDate = dto.PublicationDate,
            Language = dto.Language,
            BookId = dto.BookId,
            EditorialId = dto.EditorialId,
            TypeCode = dto.TypeCode
        };
    }

    public static EditionDto ToDto(this Edition dao)
    {
        return new EditionDto
        {
            Id = dao.EditionId,
            PublicationDate = dao.PublicationDate,
            Pages = dao.Pages,
            Language = dao.Language,
            TypeCode = dao.EditionType.Code
        };
    }

    public static EditionResponseDto ToResposeDto(this Edition dao)
    {
        return new EditionResponseDto
        {
            Id = dao.EditionId,
            PublicationDate = dao.PublicationDate,
            Pages = dao.Pages,
            Language = dao.Language,
            Book = dao.Book.Title,
            TypeCode = dao.EditionType.Code,
            Editorial = dao.Editorial.Name,
            Isbn = dao.Isbn?.Code,
            Price = dao.EditionPrice!.Price
        };
    }
}
