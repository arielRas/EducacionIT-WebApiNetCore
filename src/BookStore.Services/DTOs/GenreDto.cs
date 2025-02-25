using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Services.DTOs;

public record GenreDto
{
    [Required(ErrorMessage = "The code attribute is mandatory")]
    [Length(5, 5, ErrorMessage = "The code must have 5 characters")]
    [RegularExpression(@"^[A-Z]+$", ErrorMessage = "All characters in the code must be uppercase")]
    public required string Code { get; set; }

    [Required]
    public required string Name { get; set; }
}