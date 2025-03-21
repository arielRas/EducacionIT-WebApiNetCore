using System;

namespace BookStore.Common.Exceptions;
[Serializable]
public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException() : base("The resource was not found")
    {

    }

    public ResourceNotFoundException(string message) : base(message)
    {
    }

    public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}