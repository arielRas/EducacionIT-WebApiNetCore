using BookStore.Api.Models.Mappers;
using BookStore.Common.Exceptions;
using System.Text.Json;

namespace BookStore.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static async Task WriteExceptionResponseAsync(this HttpContext context, Exception ex, Guid traceId)
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

            var apiError = ex.ToApiError(traceId, context.Request.Path.Value!, response.StatusCode);

            await response.WriteAsync(JsonSerializer.Serialize(apiError));
        }
    }
}
