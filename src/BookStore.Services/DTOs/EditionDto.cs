using System;

namespace BookStore.Services.DTOs;

public record EditionDto
{
    public Guid Id { get; set; }
    public string? Isbn { get; set; }
    public int Pages { get; set; }
    public required string Language { get; set; }
    public DateOnly PublicationDate { get; set; }
    public required BookDto Book { get; set; }
    public required EditionTypeDto Type { get; set; }
    public required EditorialDto Editorial { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}