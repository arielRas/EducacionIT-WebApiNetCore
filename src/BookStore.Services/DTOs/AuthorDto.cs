using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Services.DTOs;

public record AuthorDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public string? LastName { get; set; }
}
