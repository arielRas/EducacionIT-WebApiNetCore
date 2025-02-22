using System;
using System.Text.RegularExpressions;
using BookStore.Common.Validations;
using BookStore.Services.DTOs;

namespace BookStore.Services.Validators;

internal static class EditionValidator
{
    public static ResultValidation Validate(this EditionRequestCreateDto editionDto)
    {
        if(Regex.IsMatch(editionDto.TypeCode, @"^[A-Z]{5}$"))
            return new ResultValidation("The edition type field must have 5 uppercase alphabetical characters");

        if(editionDto.Isbn != null && editionDto.Isbn.Count() != 13)
            return new ResultValidation("The ISBN field can only contain 13 characters");

        if(editionDto.Price <= 0)
            return new ResultValidation("The price must be greater than zero");
        
        if(editionDto.BookId <= 0)
            return new ResultValidation("The book Id field must be greater than zero");

        if(editionDto.EditorialId <= 0)
            return new ResultValidation("The editorial Id field must be greater than zero");
        
        return new ResultValidation();
    }

    public static ResultValidation Validate(this EditionRequestUpdateDto editionDto)
    {
        if (Regex.IsMatch(editionDto.TypeCode, @"^[A-Z]{5}$"))
            return new ResultValidation("The edition type field must have 5 uppercase alphabetical characters");

        if (editionDto.BookId <= 0)
            return new ResultValidation("The book Id field must be greater than zero");

        if (editionDto.EditorialId <= 0)
            return new ResultValidation("The editorial Id field must be greater than zero");

        return new ResultValidation();
    }
}
