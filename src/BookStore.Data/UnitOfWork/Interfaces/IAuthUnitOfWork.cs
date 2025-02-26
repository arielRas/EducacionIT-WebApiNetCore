using System;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Data.UnitOfWork;

public interface IAuthUnitOfWork
{
    UserManager<IdentityUser> UserManager {get;}
    RoleManager<IdentityRole> RoleManager {get;}
}