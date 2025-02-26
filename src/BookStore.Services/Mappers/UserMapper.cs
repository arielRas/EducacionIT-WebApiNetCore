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
}
