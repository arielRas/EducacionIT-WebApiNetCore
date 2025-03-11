using System;
using BookStore.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Services.Handlers;

internal class CriticalExceptionHandler
{
    private readonly ILogger _logger;
    private CriticalExceptionHandler(ILogger logger)
        => _logger = logger;

    public static CriticalExceptionHandler Create(ILogger logger)
        => new CriticalExceptionHandler(logger);

    public CriticalException HandleAndThrow(Exception ex, string methodName, string message)
    {
        var traceId = Guid.NewGuid();

        _logger.LogError(ex, $"{traceId} - {message}");

        CriticalException crititcalException = ex switch
        {
            DbUpdateException => new DataBaseException(message, traceId),
            SecurityException => new SecurityException(message, traceId),
            _ => throw new NotSupportedException($"Unsupported exception type: {ex.GetType().Name}")
        };

        return crititcalException;
    }
}
