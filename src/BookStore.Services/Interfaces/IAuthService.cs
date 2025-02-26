using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IAuthService
{
    Task SignUpAsync(UserSingUpDto user);
    Task CreateRoleAsync(string roleName);
}
