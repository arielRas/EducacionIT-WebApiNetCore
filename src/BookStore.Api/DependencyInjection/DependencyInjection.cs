using System;
using System.Text;
using BookStore.Data.Databases.AuthenticationDb;
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Repository.Interfaces;
using BookStore.Data.Repository.Repositories;
using BookStore.Data.UnitOfWork;
using BookStore.Data.UnitOfWork.Implementation;
using BookStore.Data.UnitOfWork.Interfaces;
using BookStore.Services.Implementations;
using BookStore.Services.Interfaces;
using BookStore.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
        services.AddScoped<IAuthUnitOfWork, AuthUnitOfWork>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<JwtGeneratorService>();

        //Registro de servicios de autenticacion Identity
        services.AddIdentity<IdentityUser, IdentityRole>()
                        .AddEntityFrameworkStores<AuthDbContext>()
                        .AddDefaultTokenProviders();

        //Autenticacion JWT
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JWT:BookStore"],
                        ValidAudience = configuration["JWT:Users"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!))
                    };
                });

        //Se registra Swagger como explorador de Endpoints y se agrega campo para utilizar JWT
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options => // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        {
            //options.SwaggerDoc("v1", new OpenApiInfo { Title = "Biblioteca API", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter the JWT with Bearer in the field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{ }
                }
            });
        });

        return services;
    }
}