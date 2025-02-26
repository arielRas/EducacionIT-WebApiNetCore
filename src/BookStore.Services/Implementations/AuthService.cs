using System;
using System.Security.Authentication;
using BookStore.Data.UnitOfWork;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using BookStore.Services.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace BookStore.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IAuthUnitOfWork _unitOfWork;
    private readonly string _defaultRole;

    public AuthService(IAuthUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _defaultRole = configuration.GetSection("identity:DefaultRole").Value!;
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

    public async Task CreateRoleAsync(string roleName)
    {
        try
        {
            var role = new IdentityRole{ Name = roleName };

            var result = await _unitOfWork.RoleManager.CreateAsync(role);

            if(!result.Succeeded)
                throw new AuthenticationException(result.Errors.ToString());
        }       
        catch(Exception)
        {
            throw;
        } 
    }
}