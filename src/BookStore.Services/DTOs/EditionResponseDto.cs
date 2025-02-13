namespace BookStore.Services.DTOs;

public record class EditionResponseDto : EditionDto
{
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
