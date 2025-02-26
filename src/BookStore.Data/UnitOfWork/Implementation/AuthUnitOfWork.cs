using System;
using BookStore.Data.Databases.AuthenticationDb;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Data.UnitOfWork.Implementation;

public class AuthUnitOfWork : UnitOfWork<AuthDbContext>, IAuthUnitOfWork
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthUnitOfWork(AuthDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
        : base(context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public UserManager<IdentityUser> UserManager => _userManager;
    public RoleManager<IdentityRole> RoleManager => _roleManager;
}
