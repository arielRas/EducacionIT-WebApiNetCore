using BookStore.Common.Exceptions;
using BookStore.Services.Handlers;
using Microsoft.Extensions.Logging;

namespace BookStore.Services.Extensions
{
    internal static class LoggerExtensions
    {
        public static CriticalException HandleAndThrow(this ILogger logger, Exception ex, string message)
            => CriticalExceptionHandler.Create(logger).HandleAndThrow(ex, message);
    }
}
