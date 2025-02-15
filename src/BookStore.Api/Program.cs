
using BookStore.Data.Databases.BookStoreDb;
using BookStore.Data.Repository.Interfaces;
using BookStore.Data.Repository.Repositories;
using BookStore.Services.Implementations;
using BookStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Cadenas de conexión
            var bookStoreConnectionString = builder.Configuration.GetConnectionString("BookStoreDb");

            //Contextos de Base de datos
            builder.Services.AddDbContext<BookStoreDbContext>(option =>
                option.UseSqlServer(bookStoreConnectionString));

            //Inyeccion de dependencias
            builder.Services.AddScoped<IGenreRepository, GenreRepository>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<IEditionTypeRepository, EditionTypeRepository>();
            builder.Services.AddScoped<IEditionTypeService, EditionTypeService>();
            builder.Services.AddScoped<IEditorialRepository, EditorialRepository>();
            builder.Services.AddScoped<IEditorialService, EditorialService>();


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
