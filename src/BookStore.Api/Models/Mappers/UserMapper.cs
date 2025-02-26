using System;
using BookStore.Services.DTOs;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Api.Models.Mappers;

internal static class UserMapper
{
    public static IdentityUser ToIdentity(this UserSignUp dto)
    {
        return new IdentityUser
        {
            UserName = dto.Username,
            Email = dto.Email
        };
    }
}
