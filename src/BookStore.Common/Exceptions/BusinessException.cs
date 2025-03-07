using System;

namespace BookStore.Common.Exceptions;

[Serializable]
public class BusinessException : Exception
{
    public BusinessException() : base("The object you are trying to manipulate does not comply with business rules")
    {

    }

    public BusinessException(string message) : base(message)
    {
    }

    public BusinessException(string message, Exception innerException) : base(message, innerException)
    {
    }
}