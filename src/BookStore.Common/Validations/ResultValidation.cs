using System;

namespace BookStore.Common.Validations;

public class ResultValidation
{
    private readonly string? _errorMessage; 

    public ResultValidation(string? errorMessage = null)
        => _errorMessage = errorMessage;

    public string ErrorMessage 
        => _errorMessage ?? string.Empty;
        
    public bool IsValid 
        => string.IsNullOrEmpty(_errorMessage);

}
