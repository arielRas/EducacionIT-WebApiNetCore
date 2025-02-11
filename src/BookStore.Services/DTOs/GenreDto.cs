using System;

namespace BookStore.Services.DTOs;

public record GenreDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}
