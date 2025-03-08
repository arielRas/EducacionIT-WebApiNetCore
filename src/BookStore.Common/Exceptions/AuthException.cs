using System;

namespace BookStore.Common.Exceptions;

[Serializable]
public class AuthException : Exception
{
    public AuthException() : base("An error occurred while trying to authenticate your identity")
    {

    }

    public AuthException(string message) : base(message)
    {
    }

    public AuthException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
