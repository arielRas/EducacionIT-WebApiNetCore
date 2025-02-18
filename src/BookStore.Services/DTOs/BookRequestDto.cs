namespace BookStore.Services.DTOs;

public record class BookRequestDto : BookDto
{
    public required List<int> AuthorsId {get; set;}    
    public required List<string> Genres { get; set; }
}
