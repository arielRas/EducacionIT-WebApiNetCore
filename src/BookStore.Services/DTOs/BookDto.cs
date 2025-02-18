using System;

namespace BookStore.Services.DTOs;

public record BookDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Synopsis { get; set; }
}
