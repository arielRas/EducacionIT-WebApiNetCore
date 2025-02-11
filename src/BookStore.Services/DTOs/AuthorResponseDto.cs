using System;

namespace BookStore.Services.DTOs;

public record AuthorResponseDto : AuthorDto
{
    public required List<BookDto> Books { get; set; }
}
