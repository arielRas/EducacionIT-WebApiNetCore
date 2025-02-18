namespace BookStore.Services.DTOs;

public record class EditionRequestDto : EditionResponseDto
{

    public required int BookId { get; set; }
    public required int EditorialId { get; set; }   
}