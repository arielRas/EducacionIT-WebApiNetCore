using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record class EditionRequestUpdateDto : EditionDto
{
    [JsonPropertyOrder(5)]
    [Required]
    public required int BookId { get; set; }

    [JsonPropertyOrder(6)]
    [Required]
    public required int EditorialId { get; set; }
}
