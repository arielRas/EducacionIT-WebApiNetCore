using BookStore.Data.Databases.BookStoreDb.Entities;
using BookStore.Services.DTOs;

namespace BookStore.Services.Mappers;

internal static class EditionTypeMapper
{
    public static EditionType ToDao(EditionTypeDto dto)
    {
        return new EditionType
        {
            Code = dto.Code,
            Name = dto.Name
        };
    }

    public static EditionTypeDto ToDto(EditionType dao)
    {
        return new EditionTypeDto
        {
            Code = dao.Code,
            Name = dao.Name
        };
    }
}
