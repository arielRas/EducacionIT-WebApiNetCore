using System;
using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record BookResponseDto : BookDto
{
    [JsonPropertyOrder(4)]
    public required List<AuthorDto> Authors { get; set; }

    [JsonPropertyOrder(5)]
    public required List<string> Genres { get; set; }

    [JsonPropertyOrder(6)]
    public List<string>? Editions { get; set; }
}