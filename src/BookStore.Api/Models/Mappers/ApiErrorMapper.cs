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

        public static ApiErrorCritical ToApiError(this Exception ex, Guid traceId, string path, int statusCode)
        {
            return new ApiErrorCritical
            {
                Id = traceId,
                TimeStamp = DateTime.UtcNow,
                Status = statusCode,
                Path = path,
                Description = statusCode != 500
                              ? ex.Message
                              : "Unexpected error, please contact application support"
            };
        }
    }
}
