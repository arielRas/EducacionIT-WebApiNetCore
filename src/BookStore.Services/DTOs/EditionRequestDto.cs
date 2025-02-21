using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record class EditionRequestDto : EditionResponseDto
{    
    [JsonPropertyOrder(8)]
    [Required]
    public required int BookId { get; set; }

    [JsonPropertyOrder(9)]
    [Required]
    public required int EditorialId { get; set; }      
}