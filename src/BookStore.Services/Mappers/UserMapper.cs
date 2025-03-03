using System;
using BookStore.Services.DTOs;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Services.Mappers;

internal static class UserMapper
{
    public static IdentityUser ToDao(this UserSingUpDto dto)
    {
        return new IdentityUser
        {
            UserName = dto.Username,
            Email = dto.Email
        };
    }

    public static UserLoggedDto ToDto(this IdentityUser dao, IEnumerable<string> roles)
    {
        return new UserLoggedDto
        {
            Id = dao.Id,
            Username = dao.UserName!,
            Roles = roles.ToList()
        };
    }
}