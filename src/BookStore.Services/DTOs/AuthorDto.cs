using System;

namespace BookStore.Services.DTOs;

public record AuthorDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? lastName { get; set; }
}
