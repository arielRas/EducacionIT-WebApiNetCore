using System;
using System.Text.Json;
using BookStore.Api.Models;
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
            var errorId = Guid.NewGuid();

            _logger.LogError(ex, $"ErrorId: {errorId}: Unexpected error occurred.");

            await HandleExceptionAsync(context, ex, errorId);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex, Guid errorId)
    {
        var response = context.Response;

        response.ContentType = "application/json";

        response.StatusCode = ex switch
        {
            ResourceNotFoundException => StatusCodes.Status404NotFound,
            BusinessException => StatusCodes.Status400BadRequest,
            AuthException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        var error = new ApiErrorCritical
        {
            Id = errorId,
            TimeStamp = DateTime.UtcNow,
            Path = context.Request.Path.Value,
            Status = response.StatusCode,
            Description = response.StatusCode != 500 
                          ? ex.Message 
                          : "Unexpected error, please contact application support"  
        };

        return response.WriteAsync(JsonSerializer.Serialize(error));
    }
}
