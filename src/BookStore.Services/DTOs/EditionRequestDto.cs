using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Services.DTOs;

public record class EditionRequestDto : EditionDto
{   
    [JsonPropertyOrder(5)]
    public string? Isbn { get; set; }

    [Required]
    [JsonPropertyOrder(6)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [JsonPropertyOrder(7)]
    [Required]
    public required int BookId { get; set; }

    [JsonPropertyOrder(8)]
    [Required]
    public required int EditorialId { get; set; }      
}