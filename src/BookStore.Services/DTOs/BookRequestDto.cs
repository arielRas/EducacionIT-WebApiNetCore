using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record class BookRequestDto : BookDto
{
    [JsonPropertyOrder(4)]
    public required List<int> AuthorsId {get; set;}

    [JsonPropertyOrder(5)]
    public required List<string> Genres { get; set; }
}
