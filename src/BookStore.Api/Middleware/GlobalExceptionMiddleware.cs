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
            var errorId = Guid.NewGuid();

            _logger.LogError(ex, $"Unexpected error occurred. ErrorId: {errorId}");

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
            AutenticationException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        var error = new
        {
            ErrorId = errorId,
            TimeStamp = DateTime.UtcNow,
            Path = context.Request.Path.Value,
            StatusCode = response.StatusCode,
            Message = response.StatusCode != 500 
                      ? ex.Message :
                      "Unexpected error, please contact application support"            
        };

        return response.WriteAsync(JsonSerializer.Serialize(error));
    }
}
