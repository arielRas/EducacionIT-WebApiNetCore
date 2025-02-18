using System;

namespace BookStore.Services.DTOs;

public record EditionDto
{
    public Guid Id { get; set; } 
    public DateOnly PublicationDate { get; set; }
    public int Pages { get; set; }    
    public required string Language { get; set; }
    public required string TypeCode { get; set; }
    public required string Editorial { get; set; }    
}