using System;
using BookStore.Services.DTOs;

namespace BookStore.Services.Interfaces;

public interface IAuthService
{
    Task SignUpAsync(UserSingUpDto user);
    Task<string> GetJwtTokenAsync(string userName);
    Task<RoleDto> CreateRoleAsync(string roleName);
    Task<RoleDto> GetRoleByIdAsync(Guid id);
}
