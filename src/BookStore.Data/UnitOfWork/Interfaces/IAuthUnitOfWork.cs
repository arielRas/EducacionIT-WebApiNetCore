using System;
using BookStore.Data.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Data.UnitOfWork;

public interface IAuthUnitOfWork : IUnitOfWork
{
    UserManager<IdentityUser> UserManager {get;}
    RoleManager<IdentityRole> RoleManager {get;}
}