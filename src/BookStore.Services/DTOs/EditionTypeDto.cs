using System;

namespace BookStore.Services.DTOs;

public record EditionTypeDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}
