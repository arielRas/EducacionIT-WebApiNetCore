using AspNetCoreRateLimit;
using BookStore.Api.DependencyInjection;
using BookStore.Api.Middleware;

namespace BookStore.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Servicos de la app
        builder.Services.AddAplicactionServices(builder.Configuration)
                        .AddPresentation();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<GlobalExceptionMiddleware>();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseIpRateLimiting();

        app.MapControllers();

        app.Run();
    }
}
