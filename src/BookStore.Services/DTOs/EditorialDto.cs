using System;

namespace BookStore.Services.DTOs;

public record EditorialDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
