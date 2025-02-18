namespace BookStore.Services.DTOs;

public record class EditionResponseDto : EditionDto
{
    
    public string? Isbn { get; set; } 
    public decimal Price { get; set; }
}