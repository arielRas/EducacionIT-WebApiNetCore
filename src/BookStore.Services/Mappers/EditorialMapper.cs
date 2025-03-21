using System;
using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Services.DTOs;

namespace BookStore.Services.Mappers;

internal static class EditorialMapper
{
    public static Editorial ToDao(this EditorialDto dto)
    {
        return new Editorial
        {
            EditorialId = dto.Id,
            Name = dto.Name
        };
    }

    public static EditorialDto ToDto(this Editorial dao)
    {
        return new EditorialDto
        {
            Id = dao.EditorialId,
            Name = dao.Name
        };
    }
}
