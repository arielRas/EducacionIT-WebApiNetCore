using System;
using BookStore.Data.Databases.AuthenticationDb;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Repository.Interfaces;
using BookStore.Data.Repository.Repositories;
using BookStore.Data.UnitOfWork.Implementation;
using BookStore.Data.UnitOfWork.Interfaces;
using BookStore.Services.Implementations;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddAplicactionServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Contextos de Base de datos
        services.AddDbContext<BookStoreDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("BookStoreDb")));
        
        services.AddDbContext<AuthDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("AuthenticationDb")));

        //Registro de servicios de autenticacion
        services.AddIdentity<IdentityUser, IdentityRole>()
                        .AddEntityFrameworkStores<AuthDbContext>()
                        .AddDefaultTokenProviders();

        //Registro de servicios
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IEditionTypeRepository, EditionTypeRepository>();
        services.AddScoped<IEditionTypeService, EditionTypeService>();
        services.AddScoped<IEditorialRepository, EditorialRepository>();
        services.AddScoped<IEditorialService, EditorialService>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IBookUnitOfWork, BookUnitOfWork>();
        services.AddScoped<IEditionRepository, EditionRepository>();
        services.AddScoped<IEditionService, EditionService>();
        services.AddScoped<IEditionUnitOfWork, EditionUnitOfWork>();


        return services;
    }
}