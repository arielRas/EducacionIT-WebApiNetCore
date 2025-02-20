using System;
using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record AuthorResponseDto : AuthorDto
{
    [JsonPropertyOrder(4)]
    public List<BookDto>? Books { get; set; }
}
