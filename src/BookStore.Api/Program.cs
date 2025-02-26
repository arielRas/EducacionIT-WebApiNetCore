
using BookStore.Api.DependencyInjection;
using BookStore.Data.Databases.AuthenticationDb;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Servicos de la app
            builder.Services.AddAplicactionServices(builder.Configuration)
                            .AddControllers();           


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

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
