using System;
using System.Text.Json;
using BookStore.Common.Exceptions;

namespace BookStore.Api.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "unexpected error");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var response = context.Response;

        response.ContentType = "application/json";

        response.StatusCode = ex switch
        {
            ResourceNotFoundException => StatusCodes.Status404NotFound,
            BusinessException => StatusCodes.Status400BadRequest,
            AutenticationException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        var error = new
        {
            message = ex.Message,
            statusCode = response.StatusCode
        };

        return response.WriteAsync(JsonSerializer.Serialize(error));
    }
}
