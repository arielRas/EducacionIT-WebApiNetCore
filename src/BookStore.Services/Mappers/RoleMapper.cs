using System;
using BookStore.Services.DTOs;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Services.Mappers;

internal static class RoleMapper
{
    public static RoleDto ToDto(this IdentityRole dao)
    {
        return new RoleDto
        {
            Id = Guid.Parse(dao.Id),
            Name = dao.Name!
        };
    }
}
