using System;

namespace BookStore.Services.DTOs;

public record BookDto
{
    public int Id { get; set; }
    public int Title { get; set; }
    public string? Synopsis { get; set; }  
}
