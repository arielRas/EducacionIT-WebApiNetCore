using System;

namespace BookStore.Services.DTOs;

public record BookResponseDto : BookDto
{     
    public required List<GenreDto> Genres { get; set; }
    public required List<AuthorDto> Authors { get; set; }
    public required List<string> Editions { get; set; }
}
