using System;
using System.Text.Json;
using BookStore.Api.Extensions;
using BookStore.Api.Models;
using BookStore.Api.Models.Mappers;
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
            var traceId = Guid.NewGuid();

            _logger.LogError(
                ex, $"ErrorId: {traceId} - Unexpected error occurred in {context.Request.Path.Value!}.");

            await context.WriteExceptionResponseAsync(ex, traceId);
        }
    }
}
