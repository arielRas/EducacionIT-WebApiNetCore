using System;

namespace BookStore.Services.DTOs;

public record AuthorResponseDto : AuthorDto
{
    public List<BookDto>? Books { get; set; }
}
