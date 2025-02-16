using System;

namespace BookStore.Services.DTOs;

public record AuthorDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? LastName { get; set; }
}
