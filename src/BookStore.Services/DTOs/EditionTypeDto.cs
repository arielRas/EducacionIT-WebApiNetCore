using System;

namespace BookStore.Services.DTOs;

public record EditionTypeDto
{
    public required string Code { get; set; }
    public required string Name { get; set; }
}
