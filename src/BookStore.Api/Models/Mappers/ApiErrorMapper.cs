using BookStore.Common.Exceptions;

namespace BookStore.Api.Models.Mappers
{
    internal static class ApiErrorMapper
    {
        public static ApiError ToApiError(this Exception ex, string path, int statusCode) 
        {
            return new ApiError
            {
               TimeStamp = DateTime.UtcNow,
               Status = statusCode,
               Path = path,
               Description = ex.Message
            };
        }

        public static ApiErrorCritical ToApiError(this CriticalException ex, string path, int statusCode)
        {
            return new ApiErrorCritical
            {
                Id = ex.TraceId,
                TimeStamp = DateTime.UtcNow,
                Status = statusCode,
                Path = path,
                Description = ex.Message
            };
        }
    }
}
