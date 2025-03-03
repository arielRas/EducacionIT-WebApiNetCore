using System;

namespace BookStore.Common.Exceptions;

public class AutenticationException : Exception
{
    public AutenticationException() : base("An error occurred while trying to authenticate your identity")
    {

    }

    public AutenticationException(string message) : base(message)
    {
    }

    public AutenticationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
