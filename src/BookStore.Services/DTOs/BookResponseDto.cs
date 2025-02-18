using System;

namespace BookStore.Services.DTOs;

public record BookResponseDto : BookDto
{       
    public required List<AuthorDto> Authors { get; set; }
    public required List<string> Genres { get; set; }
    public List<string>? Editions { get; set; }
}
