using BookStore.Common.Configuration;
using BookStore.Common.Exceptions;
using BookStore.Data.UnitOfWork;
using BookStore.Services.DTOs;
using BookStore.Services.Extensions;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;
using BookStore.Services.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace BookStore.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IAuthUnitOfWork _unitOfWork;
    private readonly JwtGeneratorService _tokenGenerator;    
    private readonly DefaultRoleSettings _defaultRole;

    public AuthService(
        IAuthUnitOfWork unitOfWork,
        IOptions<DefaultRoleSettings> defaultRolesettings,
        JwtGeneratorService tokenGenerator
    )    
    {
        _unitOfWork = unitOfWork;
        _defaultRole = defaultRolesettings.Value;
        _tokenGenerator = tokenGenerator;
    }   

    public async Task SignUpAsync(UserSingUpDto user)
    {
        try
        {
            var newUser = user.ToDao();

            await _unitOfWork.BeginTransactionAsync();

            var result = await _unitOfWork.UserManager.CreateAsync(newUser, user.Password);

            if(!result.Succeeded)
                throw new SecurityException(result.Errors.ToString()!);

            result = await _unitOfWork.UserManager.AddToRoleAsync(newUser, _defaultRole.RoleName);

            if(!result.Succeeded)
                throw new SecurityException(result.Errors.ToString()!);

            await _unitOfWork.CommitTransactionAsync();
        }      
        catch(Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }        
    }
    

    public async Task<string> GetJwtTokenAsync(string userName)
    {
        try
        {
            var user = await _unitOfWork.UserManager.FindByNameAsync(userName)
                ?? throw new SecurityException(
                    $"Inconsistency when searching for user {userName}. The user cannot be found even though they have already logged in successfully."
                );

            var roles = await _unitOfWork.UserManager.GetRolesAsync(user)
                ?? throw new SecurityException($"Error trying to find the roles for user {userName}"); 

            return _tokenGenerator.Generate(user.ToDto(roles));
        }
        catch (Exception)
        {            
            throw;
        }
    }


    public async Task<RoleDto> CreateRoleAsync(string roleName)
    {
        try
        {
            var role = new IdentityRole{ Name = roleName };

            var result = await _unitOfWork.RoleManager.CreateAsync(role);

            if(!result.Succeeded)
                throw new SecurityException(result.Errors.ToString()!);
            
            role = await _unitOfWork.RoleManager.FindByNameAsync(roleName)
                ?? throw new SecurityException($"Error trying to create a new role {roleName}");

            return role.ToDto();
        }
        catch (Exception)
        {
            throw;
        }
    }


    public async Task<RoleDto> GetRoleByIdAsync(Guid id)
    {
        try
        {
            var role = await _unitOfWork.RoleManager.FindByIdAsync(id.ToString())
                ?? throw new ResourceNotFoundException($"Role with ID {id} is not found");

            return role.ToDto();
        }
        catch(Exception)
        {
            throw;
        }        
    }
}