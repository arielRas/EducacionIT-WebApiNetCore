using System;
using System.Security.Authentication;
using BookStore.Common.Exceptions;
using BookStore.Data.UnitOfWork;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;
using BookStore.Services.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace BookStore.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IAuthUnitOfWork _unitOfWork;
    private readonly string _defaultRole;
    private readonly JwtGeneratorService _tokenGenerator;

    public AuthService(IAuthUnitOfWork unitOfWork, IConfiguration configuration, JwtGeneratorService tokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _defaultRole = configuration.GetSection("identity:DefaultRole").Value!;
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
                throw new AuthenticationException(result.Errors.ToString());

            result = await _unitOfWork.UserManager.AddToRoleAsync(newUser, _defaultRole);

            if(!result.Succeeded)
                throw new AuthenticationException(result.Errors.ToString());

            await _unitOfWork.CommitTransactionAsync();
        }
        catch(AuthenticationException)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
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
                ?? throw new ResourceNotFoundException();

            var roles = await _unitOfWork.UserManager.GetRolesAsync(user);

            return _tokenGenerator.Generate(user.ToDto(roles));
        }
        catch(ResourceNotFoundException)
        {
            throw;
        } 
        catch(Exception)
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
                throw new AuthenticationException(result.Errors.ToString());
            
            role = await _unitOfWork.RoleManager.FindByNameAsync(roleName)
                ?? throw new ResourceNotFoundException($"Role with name {roleName} is not found");

            return role.ToDto();
        }       
        catch(Exception)
        {
            throw;
        } 
    }

    public async Task<RoleDto> GetRoleByIdAsync(Guid id)
    {
        var role = await _unitOfWork.RoleManager.FindByIdAsync(id.ToString())
            ?? throw new ResourceNotFoundException($"Role with ID {id} is not found");
        
        return role.ToDto();
    }
}